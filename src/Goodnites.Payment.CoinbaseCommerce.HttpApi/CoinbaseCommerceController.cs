using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public abstract class CoinbaseCommerceController : AbpController
    {
        protected CoinbaseCommerceController()
        {
            LocalizationResource = typeof(CoinbaseCommerceResource);
        }
    }
}
