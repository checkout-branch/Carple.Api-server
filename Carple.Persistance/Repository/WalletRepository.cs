using System.Data;
using System.Data.Common;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Entities;
using Carple.Domain.Enums;
using Dapper;

namespace Carple.Persistance.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IDbConnection _db;

        public WalletRepository(IDbConnection db)
        {
            _db = db;
        }

        private async Task EnsureConnectionAsync()
        {
            if (_db.State != ConnectionState.Open)
            {
                if (_db is DbConnection dbConn)
                    await dbConn.OpenAsync();
                else
                    _db.Open();
            }
        }

        public async Task<Wallet?> GetWalletByUserIdAsync(int userId)
        {
            await EnsureConnectionAsync();
            return await _db.QuerySingleOrDefaultAsync<Wallet>(
                "EXEC SP_GetWalletBalanceById @UserId",
                new { UserId = userId });
        }

        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(int userId)
        {
            await EnsureConnectionAsync();
            return await _db.QueryAsync<WalletTransaction>(
                "EXEC SP_GetWalletTransactions @UserId",
                new { UserId = userId });
        }

        public async Task<int> PerformWalletTransactionAsync(int walletId, decimal amount, TransactionType transactionType, string description)
        {
            await EnsureConnectionAsync();
            return await _db.ExecuteAsync(
                "EXEC SP_PerformWalletTransaction @WalletId, @Amount, @TransactionType, @Description",
                new { WalletId = walletId, Amount = amount, TransactionType = (int)transactionType, Description = description });
        }

        public async Task<int> CreateWalletAsync(int userId)
        {
            await EnsureConnectionAsync();
            return await _db.ExecuteAsync(
                "EXEC SP_CreateWallet @UserId",
                new { UserId = userId });
        }

        public async Task<Ride?> GetRideByIdAsync(int rideId)
        {
            await EnsureConnectionAsync();
            return await _db.QuerySingleOrDefaultAsync<Ride>(
                "SELECT * FROM Rides WHERE RideId = @RideId",
                new { RideId = rideId });
        }

        public async Task<int> InsertPaymentAsync(Payment payment)
        {
            await EnsureConnectionAsync();
            const string sql = @"
                INSERT INTO Payments 
                    (RideId, PaymentMethod, Amount, CompanyShare, CaptainShare, PaymentStatus, TransactionId, PaidAt)
                VALUES 
                    (@RideId, @PaymentMethod, @Amount, @CompanyShare, @CaptainShare, @PaymentStatus, @TransactionId, @PaidAt)";

            return await _db.ExecuteAsync(sql, payment);
        }
    }
}
