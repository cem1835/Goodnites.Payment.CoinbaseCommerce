using EasyAbp.PaymentService.Payments;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceDomainModule),
        typeof(CoinbaseCommerceApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class CoinbaseCommerceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CoinbaseCommerceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CoinbaseCommerceApplicationModule>(validate: true);
            });
        }
    }
}
