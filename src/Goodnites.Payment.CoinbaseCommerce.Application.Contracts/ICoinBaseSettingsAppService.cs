using System.Threading.Tasks;
using Goodnites.Payment.CoinbaseCommerce.Dto;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public interface ICoinBaseSettingsAppService
    {
        Task<CoinBaseSettingsDto> GetAsync();

        Task UpdateAsync(CoinBaseSettingsDto input);
    }
}