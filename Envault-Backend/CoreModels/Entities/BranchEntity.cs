using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    [Table("Envault_Tbl_BranchesAvailable")]
    public class BranchEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchID { get; set; }
        public string BranchLocatedState { get; set; }
        public string City { get; set; }
        public string BranchAddress { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public virtual ICollection<AccountsEntity> Accounts { get; set; }
    }
}
