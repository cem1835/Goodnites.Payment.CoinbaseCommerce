using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using EasyAbp.PaymentService.Payments;
using Goodnites.Payment.CoinbaseCommerce.CoinBase;
using Goodnites.Payment.CoinbaseCommerce.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;
using CommerceApi = Goodnites.Payment.CoinbaseCommerce.CoinBase.CommerceApi;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public class CoinBaseApiService : ICoinBaseApiService
    {
        private readonly CommerceApi _commerceApi;
        private readonly IPaymentManager _paymentManager;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ISettingManager _settingManager;
        private readonly ICoinbasePaymentRepository _coinbasePaymentRepository;
        private readonly ILogger<CoinBaseApiService> _logger;

        public CoinBaseApiService(CommerceApi commerceApi,
            IPaymentManager paymentManager,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            IDistributedEventBus distributedEventBus,
            IGuidGenerator guidGenerator,
            ISettingManager settingManager,
            ICoinbasePaymentRepository coinbasePaymentRepository, ILogger<CoinBaseApiService> logger)
        {
            _commerceApi = commerceApi;
            _paymentManager = paymentManager;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
            _distributedEventBus = distributedEventBus;
            _guidGenerator = guidGenerator;
            _settingManager = settingManager;
            _coinbasePaymentRepository = coinbasePaymentRepository;
            _logger = logger;
        }

        public async Task<Response<Charge>> CreateChargeAsync(CreateCharge createCharge)
        {
            var matchKey = _guidGenerator.Create();

            createCharge.Metadata[CoinBaseConsts.MatchKey] = matchKey;
            
            var result = await _commerceApi.CreateChargeAsync(createCharge);
            
            if (result.HasError())
            {
                _logger.LogError($"Coinbase Payment Error : {result.Error.Message}");
                throw new UserFriendlyException(result.Error.Message, result.Error.Type, logLevel: LogLevel.Error);
            }

            try
            {
                var createPaymentEto = new CreatePaymentEto(
                    _currentTenant.Id,
                    _currentUser.GetId(),
                    CoinBaseConsts.CoinBase,
                    createCharge.LocalPrice.Currency,
                    new List<CreatePaymentItemEto>()
                    {
                        new CreatePaymentItemEto
                        {
                            ItemType = "FUND",
                            ItemKey = matchKey.ToString(),
                            OriginalPaymentAmount = createCharge.LocalPrice.Amount,
                        }
                    }
                );

                createPaymentEto.SetProperty("FUND", true);
                createPaymentEto.SetProperty(CoinBaseConsts.MatchKey, matchKey);
                
                await _distributedEventBus.PublishAsync(createPaymentEto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"CoinBase Event Error");
            }
            
            return result;
        }

        public async Task WebHookAsync(string webhook, string headerValue)
        {
            var webHook =
                await _settingManager.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseWebHookKey, "G", null, true);

            if (WebhookHelper.IsValid(webHook, headerValue, webhook))
            {
                var parsedWebHook = JsonConvert.DeserializeObject<Webhook>(webhook);
                
                var chargeInfo = parsedWebHook.Event.DataAs<Charge>();

                var matchKey = chargeInfo.Metadata[CoinBaseConsts.MatchKey].ToObject<Guid>();

                var userId = chargeInfo.Metadata[CoinBaseConsts.CustomerId].ToObject<Guid>();

                var coinBasePayments =
                    await _coinbasePaymentRepository.GetCoinBasePaymentsByUserIdAsync(userId);

                var payment =
                    coinBasePayments.FirstOrDefault(x =>x.GetProperty(CoinBaseConsts.MatchKey,Guid.Empty) == matchKey);

                if (payment.GetProperty(CoinBaseConsts.WebHookId,"")==parsedWebHook.Id) // already processessed
                {
                    return;
                }
                
                if (parsedWebHook.Event.IsChargeFailed)
                {
                    await _paymentManager.StartCancelAsync(payment);
                }
                else if (parsedWebHook.Event.IsChargeConfirmed)
                {
                    var extraPropertyDictionary = new ExtraPropertyDictionary()
                    {
                        {CoinBaseConsts.CustomerId, userId},
                        {CoinBaseConsts.WebHookId,parsedWebHook.Id}
                    };
                    
                    await _paymentManager.StartPaymentAsync(payment,extraPropertyDictionary);
                }
            }
        }

        public async Task<(int Min, int Max)> GetCoinBaseMinMaxValuesAsync()
        {
            var min =
                await _settingManager.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseMin, "G", null, true);
            
            var max=await _settingManager.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseMax, "G", null, true);

            return (int.Parse(min), int.Parse(max));
        }
    }
}