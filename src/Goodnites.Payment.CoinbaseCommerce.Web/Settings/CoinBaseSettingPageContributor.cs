using System.Threading.Tasks;
using Goodnites.Payment.CoinbaseCommerce.Localization;
using Goodnites.Payment.CoinbaseCommerce.Permissions;
using Goodnites.Payment.CoinbaseCommerce.Web.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Goodnites.Payment.CoinbaseCommerce.Web.Settings
{
    public class CoinBaseSettingPageContributor:ISettingPageContributor
    {
        public Task ConfigureAsync(SettingPageCreationContext context)
        {
            var localizer = context.ServiceProvider.GetRequiredService<IStringLocalizer<CoinbaseCommerceResource>>();
            
            context.Groups.Add(new SettingPageGroup("CoinBaseSettings", localizer["CoinBase Settings"], typeof(CoinBaseSettingsViewComponent)));

            return Task.CompletedTask;
        }

        public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            return await authorizationService.IsGrantedAsync(CoinbaseCommercePermissions.ApiSetting);
        }
    }
}