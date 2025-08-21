using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels
{
    public class GenericResponse
    {
        public bool Status { get; set; }
        public Object? Data { get; set; }
        public Error? Error { get; set; }
    }
    public class Error
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
