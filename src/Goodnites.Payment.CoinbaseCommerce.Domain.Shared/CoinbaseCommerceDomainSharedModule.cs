using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class CoinbaseCommerceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CoinbaseCommerceDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<CoinbaseCommerceResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/CoinbaseCommerce");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("CoinbaseCommerce", typeof(CoinbaseCommerceResource));
            });
        }
    }
}
