using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(CoinbaseCommerceDomainSharedModule)
    )]
    public class CoinbaseCommerceDomainModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            
        }
    }
}
