using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    [DependsOn(
        typeof(CoinbaseCommerceDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class CoinbaseCommerceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CoinbaseCommerceDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}