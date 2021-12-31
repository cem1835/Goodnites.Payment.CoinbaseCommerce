using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public class CoinbasePaymentServiceProvider:PaymentServiceProvider
    {

        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentTenant _currentTenant;
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentRepository _paymentRepository;
        
        public static string PaymentMethod = "Coinbase";

        public CoinbasePaymentServiceProvider(IGuidGenerator guidGenerator, ICurrentUser currentUser, ICurrentTenant currentTenant, IPaymentManager paymentManager, IPaymentRepository paymentRepository)
        {
            _guidGenerator = guidGenerator;
            _currentUser = currentUser;
            _currentTenant = currentTenant;
            _paymentManager = paymentManager;
            _paymentRepository = paymentRepository;
        }

        public override async Task OnPaymentStartedAsync(EasyAbp.PaymentService.Payments.Payment payment, ExtraPropertyDictionary configurations)
        {
            if (payment.ActualPaymentAmount <= decimal.Zero)
            {
                throw new PaymentAmountInvalidException(payment.ActualPaymentAmount, PaymentMethod);
            }
            
            if (!Guid.TryParse(configurations.GetOrDefault(CoinBaseConsts.CustomerId) as string, out var accountId))
            {
                throw new ArgumentNullException(CoinBaseConsts.CustomerId);
            }
            
            payment.SetProperty("AccountId", accountId);
            payment.SetProperty(CoinBaseConsts.WebHookId, configurations.GetOrDefault(CoinBaseConsts.WebHookId));
            
            await _paymentManager.CompletePaymentAsync(payment); // this func publish PaymentCompletedEto 

            await _paymentRepository.UpdateAsync(payment, true);
            
        }

        public override async Task OnCancelStartedAsync(EasyAbp.PaymentService.Payments.Payment payment)
        {
            if (payment.CanceledTime.HasValue)
            {
                return;
            }
            
            await _paymentManager.CompleteCancelAsync(payment);
        }

        public override Task OnRefundStartedAsync(EasyAbp.PaymentService.Payments.Payment payment, Refund refund)
        {
            // TODO :

            return Task.CompletedTask;
        }
        
    }
}