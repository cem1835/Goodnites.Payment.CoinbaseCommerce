using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [DependsOn(
        typeof(CoinbaseCommerceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class CoinbaseCommerceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "CoinbaseCommerce";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(CoinbaseCommerceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
