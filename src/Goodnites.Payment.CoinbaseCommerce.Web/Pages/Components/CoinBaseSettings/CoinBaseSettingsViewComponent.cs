using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;

namespace Goodnites.Payment.CoinbaseCommerce.Web.Pages.Components
{
    public class CoinBaseSettingsViewComponent:AbpViewComponent
    {
        private readonly ICoinBaseSettingsAppService _coinBaseSettingsAppService;

        public CoinBaseSettingsViewComponent( ICoinBaseSettingsAppService coinBaseSettingsAppService)
        {
            _coinBaseSettingsAppService = coinBaseSettingsAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _coinBaseSettingsAppService.GetAsync();
            
            return View("~/Pages/Components/CoinBaseSettings/Default.cshtml",model);
        }
    }
}