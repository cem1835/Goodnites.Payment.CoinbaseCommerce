using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Goodnites.Payment.CoinbaseCommerce.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CoinbaseCommercePageModel : AbpPageModel
    {
        protected CoinbaseCommercePageModel()
        {
            LocalizationResourceType = typeof(CoinbaseCommerceResource);
            ObjectMapperContext = typeof(CoinbaseCommerceWebModule);
        }
    }
}