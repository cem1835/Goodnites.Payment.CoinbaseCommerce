using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using Volo.Abp.DependencyInjection;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public interface ICoinBaseApiService:ITransientDependency
    {
        Task<Response<Charge>> CreateChargeAsync(CreateCharge createCharge);

        Task WebHookAsync(Webhook webhook, string headerValue);
    }
}