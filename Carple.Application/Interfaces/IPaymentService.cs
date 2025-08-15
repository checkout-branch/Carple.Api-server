using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponse<IEnumerable<Payment>>> GetAllPaymentsAsync();
        Task<ApiResponse<Payment>> GetPaymentByIdAsync(int paymentId);
        Task<ApiResponse<string>> CreatePaymentAsync(Payment payment);
    }
}
