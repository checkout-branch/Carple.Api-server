using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Application.Interfaces;
using Carple.Domain.Entities;

namespace Carple.Insfrastructure.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ApiResponse<IEnumerable<Payment>>> GetAllPaymentsAsync()
        {
            var data = await _paymentRepository.GetAllAsync();
            return new ApiResponse<IEnumerable<Payment>>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<Payment>> GetPaymentByIdAsync(int paymentId)
        {
            var data = await _paymentRepository.GetByIdAsync(paymentId);
            if (data == null)
                return new ApiResponse<Payment>(false, "Payment not found", null);

            return new ApiResponse<Payment>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<string>> CreatePaymentAsync(Payment payment)
        {
            await _paymentRepository.CreateAsync(payment);
            return new ApiResponse<string>(true, "Payment created successfully", null);
        }
    }
}
