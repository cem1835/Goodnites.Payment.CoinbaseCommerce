using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class CoinbaseCommerceApplicationContractsModule : AbpModule
    {

    }
}
