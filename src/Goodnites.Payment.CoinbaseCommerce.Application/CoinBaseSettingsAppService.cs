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
                Min = await SettingProvider.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseMin),
                Max = await SettingProvider.GetOrNullAsync(CoinbaseCommerceSettings.CoinBaseMax)
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
                
                settingsDto.Min = await SettingManager.GetOrNullForTenantAsync(
                    CoinbaseCommerceSettings.CoinBaseMin,
                    CurrentTenant.GetId(), false);
                
                settingsDto.Max = await SettingManager.GetOrNullForTenantAsync(
                    CoinbaseCommerceSettings.CoinBaseMax,
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
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, CoinbaseCommerceSettings.CoinBaseMin,
                    input.Min);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, CoinbaseCommerceSettings.CoinBaseMax,
                    input.Max);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, CoinbaseCommerceSettings.CoinBaseWebHookKey,
                    input.WebHook);
            }
            else
            {
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseApiKey, input.ApiKey);
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseWebHookKey, input.WebHook);
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseMin, input.Min);
                await SettingManager.SetGlobalAsync(CoinbaseCommerceSettings.CoinBaseMax, input.Max);
            }
        }
    }
}