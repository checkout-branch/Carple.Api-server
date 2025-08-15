using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Enums
{
    public enum TransactionType
    {
        Debit = 0,   // Deduct funds from the wallet
        Credit = 1   // Add funds to the wallet
    }
}
