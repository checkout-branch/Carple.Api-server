using Carple.Application.Common;
using Carple.Application.Interfaces.Repositories;
using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;
using Carple.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Insfrastructure.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository) => _walletRepository = walletRepository;

        public async Task<ApiResponse<Wallet>> GetWalletBalanceAsync(int userId)
        {
            if (userId <= 0) return new ApiResponse<Wallet>(false, "Invalid UserId", null);

            var wallet = await _walletRepository.GetWalletByUserIdAsync(userId);
            if (wallet == null) return new ApiResponse<Wallet>(false, "Wallet not found", null);

            return new ApiResponse<Wallet>(true, "Wallet retrieved successfully", wallet);
        }

        public async Task<ApiResponse<IEnumerable<WalletTransaction>>> GetWalletTransactionsAsync(int userId)
        {
            if (userId <= 0) return new ApiResponse<IEnumerable<WalletTransaction>>(false, "Invalid UserId", null);

            var transactions = await _walletRepository.GetWalletTransactionsAsync(userId);
            return new ApiResponse<IEnumerable<WalletTransaction>>(true, "Transactions retrieved successfully", transactions);
        }

        public async Task<ApiResponse<string>> PerformWalletTransactionAsync(int walletId, decimal amount, TransactionType transactionType, string description)
        {
            if (walletId <= 0) return new ApiResponse<string>(false, "Invalid WalletId", null);
            if (amount <= 0) return new ApiResponse<string>(false, "Amount must be greater than zero", null);

            await _walletRepository.PerformWalletTransactionAsync(walletId, amount, transactionType, description);
            return new ApiResponse<string>(true, "Transaction completed successfully", "Transaction successful");
        }

        public async Task<ApiResponse<string>> CreateWalletAsync(int userId)
        {
            if (userId <= 0) return new ApiResponse<string>(false, "Invalid UserId", null);

            await _walletRepository.CreateWalletAsync(userId);
            return new ApiResponse<string>(true, "Wallet created successfully", "Wallet created");
        }

        public async Task<ApiResponse<string>> FinishRidePaymentWithWalletAsync(int rideId)
        {
            if (rideId <= 0) return new ApiResponse<string>(false, "Invalid RideId", null);

            Ride ride = await _walletRepository.GetRideByIdAsync(rideId);
            if (ride == null) return new ApiResponse<string>(false, "Ride not found", null);

            Wallet userWallet = await _walletRepository.GetWalletByUserIdAsync(ride.UserId);
            Wallet captainWallet = await _walletRepository.GetWalletByUserIdAsync(ride.CaptainId);

            if (userWallet == null || captainWallet == null)
                return new ApiResponse<string>(false, "Wallet(s) not found", null);

            if (userWallet.Balance < ride.ActualFare)
                return new ApiResponse<string>(false, "Insufficient balance", null);

            decimal companyShare = Math.Round(ride.ActualFare.Value * 0.2m, 2);
            decimal captainShare = ride.ActualFare.Value - companyShare;

            await _walletRepository.PerformWalletTransactionAsync(userWallet.WalletId, ride.ActualFare.Value, TransactionType.Debit, $"Fare for Ride #{rideId}");
            await _walletRepository.PerformWalletTransactionAsync(captainWallet.WalletId, captainShare, TransactionType.Credit, $"Earnings from Ride #{rideId}");

            Payment payment = new Payment
            {
                RideId = rideId,
                PaymentMethod = "Wallet",
                Amount = ride.ActualFare.Value,
                CompanyShare = companyShare,
                CaptainShare = captainShare,
                PaymentStatus = "Paid",
                TransactionId = $"TXN-{Guid.NewGuid()}",
                PaidAt = DateTime.UtcNow
            };

            await _walletRepository.InsertPaymentAsync(payment);

            return new ApiResponse<string>(true, "Ride payment completed via wallet", $"Payment TXN: {payment.TransactionId}");
        }
    }
}
