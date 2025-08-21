using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_CustomerAddress")]
    public class CustomerAddressEntity
    {
        [Key]
        [ForeignKey("BasicDetailsEntity")]
        public long CustomerId { get; set; }
        public string PermanentHouseNo { get; set; }
        public string PermanentStreet { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public int PermanentPincode { get; set; }
        public string PresentHouseNo { get; set; }
        public string PresentStreet { get; set; }
        public string PresentCity { get; set; }
        public string PresentState { get; set; }
        public int PresentPincode { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public BasicDetailsEntity? BasicDetails { get; set; }
    }
}
