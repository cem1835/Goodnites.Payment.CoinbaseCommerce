using Localization.Resources.AbpUi;
using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class CoinbaseCommerceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CoinbaseCommerceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CoinbaseCommerceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
