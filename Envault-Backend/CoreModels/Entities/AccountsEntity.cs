using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_Accounts")]
    public class AccountsEntity
    {
        [Key]
        public long AccountNumber { get; set; }
        public long CustomerId { get; set; }
        public int BranchID { get; set; }
        public double AccountBalance { get; set; }
        public int AccountTypeId { get; set; }
        public bool FreezeWithdrawl { get; set; } = false;
        public double MaxBalanceWarningAmount { get; set; } = 0;
        public double MinBalanceWarningAmount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public BasicDetailsEntity? BasicDetails { get; set; }
        public BranchEntity? Branches { get; set; }
        public AccountTypeEntity? AccountTypes { get; set; }
        public ICollection<TransactionsEntity>? SenderTransactions { get; set; }
        public ICollection<TransactionsEntity>? ReceiverTransactions { get; set; }
    }
}
