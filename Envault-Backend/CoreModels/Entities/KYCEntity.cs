using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_KYC")]
    public class KYCEntity
    {
        [Key]
        public long CustomerId { get; set; }
        public byte[] AadharCard { get; set; }
        public byte[] PANCard { get; set; }
        public byte[] CustomerPhoto { get; set; }
        public string KYCStatus {  get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public virtual BasicDetailsEntity? BasicDetails { get; set; }
    }
}
