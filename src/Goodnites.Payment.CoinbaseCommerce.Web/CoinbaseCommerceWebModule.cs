using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Goodnites.Payment.CoinbaseCommerce.Localization;
using Goodnites.Payment.CoinbaseCommerce.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Goodnites.Payment.CoinbaseCommerce.Permissions;

namespace Goodnites.Payment.CoinbaseCommerce.Web
{
    [DependsOn(
        typeof(CoinbaseCommerceHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class CoinbaseCommerceWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(CoinbaseCommerceResource), typeof(CoinbaseCommerceWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CoinbaseCommerceWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new CoinbaseCommerceMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CoinbaseCommerceWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<CoinbaseCommerceWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CoinbaseCommerceWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
