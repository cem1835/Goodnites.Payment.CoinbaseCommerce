using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    [ConnectionStringName(CoinbaseCommerceDbProperties.ConnectionStringName)]
    public interface ICoinbaseCommerceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}