using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;
using Carple.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService) => _walletService = walletService;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWalletBalance(int userId)
        {
            var response = await _walletService.GetWalletBalanceAsync(userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpGet("transactions/{userId}")]
        public async Task<IActionResult> GetWalletTransactions(int userId)
        {
            var response = await _walletService.GetWalletTransactionsAsync(userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> PerformWalletTransaction([FromBody] WalletTransactionRequest request, TransactionType transactionType)
        {
            var response = await _walletService.PerformWalletTransactionAsync(
                request.WalletId, request.Amount, transactionType, request.Description);

            return StatusCode(response.Success ? 200 : 400, response);
        }

      
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] WalletTransactionRequest request)
        {
            var response = await _walletService.PerformWalletTransactionAsync(
                request.WalletId, request.Amount, TransactionType.Credit, request.Description);

            return StatusCode(response.Success ? 200 : 400, response);
        }

 
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WalletTransactionRequest request)
        {
            var response = await _walletService.PerformWalletTransactionAsync(
                request.WalletId, request.Amount, TransactionType.Debit, request.Description);

            return StatusCode(response.Success ? 200 : 400, response);
        }




        [HttpPost("create")]
        public async Task<IActionResult> CreateWallet(int userId)
        {
            var response = await _walletService.CreateWalletAsync(userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpPost("ride-payment/{rideId}")]
        public async Task<IActionResult> FinishRidePayment(int rideId)
        {
            var response = await _walletService.FinishRidePaymentWithWalletAsync(rideId);
            return StatusCode(response.Success ? 200 : 400, response);
        }
    }
}
