using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ApiResponse<IEnumerable<PaymentEntity>>> GetAllPaymentsAsync();
        Task<ApiResponse<PaymentEntity>> GetPaymentByIdAsync(int paymentId);
        Task<ApiResponse<string>> CreatePaymentAsync(PaymentEntity payment);
    }
}
