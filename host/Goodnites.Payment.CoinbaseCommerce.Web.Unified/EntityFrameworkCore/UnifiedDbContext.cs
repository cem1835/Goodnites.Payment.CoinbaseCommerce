using EasyAbp.PaymentService.EntityFrameworkCore;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Goodnites.Payment.CoinbaseCommerce.EntityFrameworkCore
{
    public class UnifiedDbContext : AbpDbContext<UnifiedDbContext>
    {
        public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureFeatureManagement();
            modelBuilder.ConfigureTenantManagement();


            var options = new CoinbaseCommerceModelBuilderConfigurationOptions(
                CoinbaseCommerceDbProperties.DbTablePrefix,
                CoinbaseCommerceDbProperties.DbSchema
            );

            modelBuilder.Entity<EasyAbp.PaymentService.Payments.Payment>(b =>
            {
                b.ToTable(options.TablePrefix + "Payments", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
                b.Property(x => x.ActualPaymentAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.OriginalPaymentAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.PaymentDiscount).HasColumnType("numeric(20,8)");
                b.Property(x => x.RefundAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.PendingRefundAmount).HasColumnType("numeric(20,8)");
            });

            modelBuilder.Entity<PaymentItem>(b =>
            {
                b.ToTable(options.TablePrefix + "PaymentItems", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
                b.Property(x => x.ActualPaymentAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.OriginalPaymentAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.PaymentDiscount).HasColumnType("numeric(20,8)");
                b.Property(x => x.RefundAmount).HasColumnType("numeric(20,8)");
                b.Property(x => x.PendingRefundAmount).HasColumnType("numeric(20,8)");
            });
            
            modelBuilder.Entity<Refund>(b =>
            {
                b.ToTable(options.TablePrefix + "Refunds", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
                b.Property(x => x.RefundAmount).HasColumnType("numeric(20,8)");
            });
            
            modelBuilder.Entity<RefundItem>(b =>
            {
                b.ToTable(options.TablePrefix + "RefundItems", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
                b.Property(x => x.RefundAmount).HasColumnType("decimal(20,8)");
            });
        }
    }
}
