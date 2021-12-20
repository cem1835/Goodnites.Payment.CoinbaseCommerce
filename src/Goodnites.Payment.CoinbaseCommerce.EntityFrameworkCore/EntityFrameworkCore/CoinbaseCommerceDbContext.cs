using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    [ConnectionStringName(CoinbaseCommerceDbProperties.ConnectionStringName)]
    public class CoinbaseCommerceDbContext : AbpDbContext<CoinbaseCommerceDbContext>, ICoinbaseCommerceDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public CoinbaseCommerceDbContext(DbContextOptions<CoinbaseCommerceDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCoinbaseCommerce();
        }
    }
}