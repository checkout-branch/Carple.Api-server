using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Entities
{
    public class WalletTransaction
    {
        public int TransactionId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } // "Credit" / "Debit"
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
