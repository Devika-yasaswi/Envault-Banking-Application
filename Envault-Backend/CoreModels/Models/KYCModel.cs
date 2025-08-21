using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Models
{
    public class KYCModel
    {
        public long CustomerId { get; set; }
        public string AadharCard { get; set; }
        public string PANCard { get; set; }
        public string CustomerPhoto { get; set; }
    }
}
