using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Models
{
    public class AccountDetails
    {
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double AccountBalance { get; set; }
        public bool FreezeWithdrawl { get; set; }
        public double MaxBalanceWarningAmount { get; set; } = 0;
        public double MinBalanceWarningAmount { get; set; } = 0;
    }
}
