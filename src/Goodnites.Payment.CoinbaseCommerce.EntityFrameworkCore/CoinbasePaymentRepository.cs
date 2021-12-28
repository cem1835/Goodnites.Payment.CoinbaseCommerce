using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.PaymentService.EntityFrameworkCore;
using EasyAbp.PaymentService.Payments;
using Goodnites.Payment.CoinbaseCommerce.CoinBase;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Goodnites.Payment.CoinbaseCommerce
{
    public class CoinbasePaymentRepository:PaymentRepository,ICoinbasePaymentRepository
    {
        public CoinbasePaymentRepository(IDbContextProvider<IPaymentServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<EasyAbp.PaymentService.Payments.Payment>> GetCoinBasePaymentsByUserIdAsync(Guid userId)
        {
            var dbSet =await GetDbSetAsync();

            var payments =await dbSet.Where(x => x.UserId == userId && x.PaymentMethod== CoinBaseConsts.CoinBase).ToListAsync().ConfigureAwait(false);

            return payments;
        }
    }
}