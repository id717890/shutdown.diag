using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownDiagnostic.Data
{
    public class Statement
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string FullTag { get; set; }
        public object VerifyIf { get; set; }
        public string ParamType { get; set; }
    }
}
