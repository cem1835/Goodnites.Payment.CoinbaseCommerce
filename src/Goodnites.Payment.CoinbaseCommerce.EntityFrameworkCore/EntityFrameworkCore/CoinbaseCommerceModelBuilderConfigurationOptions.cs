using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    public class CoinbaseCommerceModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public CoinbaseCommerceModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}