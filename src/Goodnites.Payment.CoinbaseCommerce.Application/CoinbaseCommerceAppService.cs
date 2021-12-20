using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.Application.Services;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public abstract class CoinbaseCommerceAppService : ApplicationService
    {
        protected CoinbaseCommerceAppService()
        {
            LocalizationResource = typeof(CoinbaseCommerceResource);
            ObjectMapperContext = typeof(CoinbaseCommerceApplicationModule);
        }
    }
}
