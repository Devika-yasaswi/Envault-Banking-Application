using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_DepositRates")]
    public class DepositRatesEntity
    {
        [Key]
        public int DepositRateId { get; set; }
        public int DepositTypeId { get; set; }
        public double NormalRate { get; set; }
        public double SeniorCitizenRate { get; set; }
        public int MaxTenureMonths { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DepositTypeEntity? DepositType { get; set; }
    }
}
