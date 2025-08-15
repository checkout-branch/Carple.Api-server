using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Enities;

namespace Carple.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [Required]
        public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User User { get; set; }  

        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }

        // Navigation property
        public ICollection<WalletTransaction> Transactions { get; set; }
    }
}
