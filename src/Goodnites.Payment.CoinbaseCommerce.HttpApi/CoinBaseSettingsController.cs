using System.Threading.Tasks;
using Goodnites.Payment.CoinbaseCommerce.Dto;
using Goodnites.Payment.CoinbaseCommerce.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [RemoteService(Name = "GoodnitesCoinBaseSetting")]
    [Microsoft.AspNetCore.Components.Route("/api/coinBaseSetting")]
    [Authorize(CoinbaseCommercePermissions.ApiSetting)]
    public class CoinBaseSettingsController : CoinbaseCommerceController, ICoinBaseSettingsAppService
    {
        private readonly ICoinBaseSettingsAppService _coinBaseSettingsAppService;

        public CoinBaseSettingsController(ICoinBaseSettingsAppService coinBaseSettingsAppService)
        {
            _coinBaseSettingsAppService = coinBaseSettingsAppService;
        }

        [HttpGet]
        public async Task<CoinBaseSettingsDto> GetAsync()
        {
            return await _coinBaseSettingsAppService.GetAsync();
        }

        [HttpPut]
        public async Task UpdateAsync(CoinBaseSettingsDto input)
        {
            await _coinBaseSettingsAppService.UpdateAsync(input);
        }
    }
}