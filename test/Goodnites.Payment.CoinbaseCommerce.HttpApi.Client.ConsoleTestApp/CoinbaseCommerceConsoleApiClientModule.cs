using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class CoinbaseCommerceConsoleApiClientModule : AbpModule
    {
        
    }
}
