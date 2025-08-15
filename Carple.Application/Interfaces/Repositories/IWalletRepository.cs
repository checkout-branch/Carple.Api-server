using Carple.Domain.Entities;
using Carple.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByUserIdAsync(int userId);
        Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(int userId);
        Task<int> PerformWalletTransactionAsync(int walletId, decimal amount, TransactionType transactionType, string description);
        Task<int> CreateWalletAsync(int userId);
        Task<Ride> GetRideByIdAsync(int rideId);
        Task<int> InsertPaymentAsync(Payment payment);
    }
}
