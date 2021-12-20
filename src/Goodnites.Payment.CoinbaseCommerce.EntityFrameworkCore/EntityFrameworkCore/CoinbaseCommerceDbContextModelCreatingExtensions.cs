using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    public static class CoinbaseCommerceDbContextModelCreatingExtensions
    {
        public static void ConfigureCoinbaseCommerce(
            this ModelBuilder builder,
            Action<CoinbaseCommerceModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CoinbaseCommerceModelBuilderConfigurationOptions(
                CoinbaseCommerceDbProperties.DbTablePrefix,
                CoinbaseCommerceDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);
            
            
            
            
        }
    }
}