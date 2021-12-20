using Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(CoinbaseCommerceEntityFrameworkCoreTestModule)
        )]
    public class CoinbaseCommerceDomainTestModule : AbpModule
    {
        
    }
}
