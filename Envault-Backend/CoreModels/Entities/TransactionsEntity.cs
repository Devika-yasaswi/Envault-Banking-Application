using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_tbl_Transactions")]
    public class TransactionsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionID { get; set; }
        public long SenderAccountNumber { get; set; }
        public long ReceiverAccountNumber { get; set; }
        public Double Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public AccountsEntity? SenderAccount { get; set; }
        public AccountsEntity? ReceiverAcccount { get; set; }
    }
}
