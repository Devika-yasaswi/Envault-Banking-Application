using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Models
{
    public class BranchDetails
    {
        public int BranchID { get; set; }
        public string BranchLocatedState { get; set; }
        public string City { get; set; }
        public string BranchAddress { get; set; }
        public string IFSCCode { get; set; }
    }
}
