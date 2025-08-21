using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_BasicDetails")]
    public class BasicDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public long AadharNumber { get; set; }
        public long MobileNumber { get; set; }
        public string PANNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string EmployementType { get; set; }
        public long AnnualIncome { get; set; }
        public bool Nationality { get; set; }
        public bool TaxResidentOfIndia { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public virtual CustomerAddressEntity? CustomerAddress { get; set; }
        public virtual CustomerFamilyAndNomineeDetailsEntity? CustomerFamilyAndNomineeDetails { get; set; }
        public virtual LoginCredentialsEntity? LoginCredentials { get; set; }
        public virtual KYCEntity? KYC { get; set; }
        public virtual ICollection<AccountsEntity>? Accounts { get; set; }
    }
}