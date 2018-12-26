using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using System.Collections.Generic;

namespace ShutdownDiagnostic
{
    public class DiagnosticViewModel : IDiagnosticViewModel
    {
        public IEnumerable<Server> VerificationList { get; set; }
    }
}
