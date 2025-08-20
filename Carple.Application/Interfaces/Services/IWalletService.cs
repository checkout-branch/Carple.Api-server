using Carple.Application.Common;
using Carple.Domain.Entities;
using Carple.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Application.Interfaces.Services
{
    public interface IWalletService
    {
        Task<ApiResponse<Wallet>> GetWalletBalanceAsync(int userId);
        Task<ApiResponse<IEnumerable<WalletTransaction>>> GetWalletTransactionsAsync(int userId);
        Task<ApiResponse<string>> PerformWalletTransactionAsync(int walletId, decimal amount, TransactionType transactionType, string description);
        Task<ApiResponse<string>> CreateWalletAsync(int userId);
        Task<ApiResponse<string>> FinishRidePaymentWithWalletAsync(int rideId);
    }
}
