using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Goodnites.Payment.CoinbaseCommerce.CoinBase
{
    public interface ICoinbasePaymentRepository:ITransientDependency
    {
        Task<List<EasyAbp.PaymentService.Payments.Payment>> GetCoinBasePaymentsByUserIdAsync(Guid userId);
    }
}