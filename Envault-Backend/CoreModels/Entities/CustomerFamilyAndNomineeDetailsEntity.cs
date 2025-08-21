using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_CustomerFamilyAndNomineeDetails")]
    public class CustomerFamilyAndNomineeDetailsEntity
    {
        [Key]
        [ForeignKey("BasicDetailsEntity")]
        public long CustomerId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public string NomineeName { get; set; }
        public DateOnly NomineeDateOfBirth { get; set; }
        public string RelationWithNominee { get; set; }
        public string NomineeHouseNo { get; set; }
        public string NomineeStreet { get; set; }
        public string NomineeCity { get; set; }
        public string NomineeState { get; set; }
        public int NomineePincode { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public BasicDetailsEntity? BasicDetails { get; set; }
    }
}
