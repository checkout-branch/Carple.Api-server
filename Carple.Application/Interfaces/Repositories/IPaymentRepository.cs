using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentEntity>> GetAllAsync();
        Task<PaymentEntity?> GetByIdAsync(int paymentId);
        Task<int> CreateAsync(PaymentEntity payment);
    }
}
