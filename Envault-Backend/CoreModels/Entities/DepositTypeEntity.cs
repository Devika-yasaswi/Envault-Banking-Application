using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_DepositType")]
    public class DepositTypeEntity
    {
        [Key]
        public int DepositTypeId { get; set; }
        public string DepositType { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public ICollection<DepositRatesEntity>? DepositRates { get; set; }
    }
}
