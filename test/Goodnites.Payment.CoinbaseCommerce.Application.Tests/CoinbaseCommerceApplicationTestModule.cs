using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceApplicationModule),
        typeof(CoinbaseCommerceDomainTestModule)
        )]
    public class CoinbaseCommerceApplicationTestModule : AbpModule
    {

    }
}
