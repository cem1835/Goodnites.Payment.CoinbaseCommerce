using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using Flurl.Http;
using Goodnites.Payment.CoinbaseCommerce.Settings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.SettingManagement;

namespace Goodnites.Payment.CoinbaseCommerce.CoinBase
{
    /// <summary>
    /// The main class to use when accessing the Coinbase Commerce API. API Docs: https://commerce.coinbase.com/docs/api/
    /// </summary>
    public class CommerceApi : ITransientDependency
    {

        public const string ApiChargeUrl = "https://api.commerce.coinbase.com/charges";
        
        
        /// <summary>
        /// API Endpoint
        /// </summary>

        public CommerceApi(ISettingManager settingManager)
        {
            InitFlurlConfig(settingManager);
        }

        private void InitFlurlConfig(ISettingManager settingManager)
        {
            var apiKey=settingManager.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseApiKey, "G", null).GetAwaiter().GetResult();
            
            FlurlHttp.ConfigureClient(ApiChargeUrl, flurClient =>
            {
                flurClient.WithHeader("Content-Type", "application/json")
                    .WithHeader("X-CC-Version", "2018-03-22")
                    .WithHeader("X-CC-Api-Key", apiKey);
            });
            
        }

        public async Task<Response<Charge>> CreateChargeAsync(CreateCharge createCharge)
        {
            var result = await ApiChargeUrl.PostJsonAsync(createCharge)
                .ReceiveJson<Response<Charge>>();

            return result;
        }
    }
}