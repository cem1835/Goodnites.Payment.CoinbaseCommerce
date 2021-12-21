using System;
using System.Threading.Tasks;
using Goodnites.Payment.CoinbaseCommerce.Dto;
using Goodnites.Payment.CoinbaseCommerce.Permissions;
using Goodnites.Payment.CoinbaseCommerce.Settings;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [Authorize(CoinbaseCommercePermissions.ApiSetting)]
    public class CoinBaseSettingsAppService : CoinbaseCommerceAppService,ICoinBaseSettingsAppService
    {
        protected readonly ISettingManager SettingManager;

        public CoinBaseSettingsAppService(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public virtual async Task<CoinBaseSettingsDto> GetAsync()
        {
            var settingsDto = new CoinBaseSettingsDto()
            {
                ApiKey = await SettingProvider.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseApiKey),
                WebHook = await SettingProvider.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseWebHookKey),
            };

            if (CurrentTenant.IsAvailable)
            {
                settingsDto.ApiKey =
                    await SettingManager.GetOrNullForTenantAsync(CoinbaseCommerceSettings.CoinBaseApiKey,
                        CurrentTenant.GetId(),
                        false);

                settingsDto.WebHook = await SettingManager.GetOrNullForTenantAsync(
                    CoinbaseCommerceSettings.CoinBaseWebHookKey,
                    CurrentTenant.GetId(), false);
            }
            return settingsDto;
        }

        public virtual async Task UpdateAsync(CoinBaseSettingsDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, CoinbaseCommerceSettings.CoinBaseApiKey,
                    input.ApiKey);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, CoinbaseCommerceSettings.CoinBaseWebHookKey,
                    input.WebHook);
            }
            else
            {
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseApiKey, input.ApiKey);
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseWebHookKey, input.WebHook);
            }
        }
    }
}