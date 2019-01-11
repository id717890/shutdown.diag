using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShutdownDiagnostic
{
    public class DiagnosticViewModel : IDiagnosticViewModel
    {
        //public IEnumerable<Server> VerificationList { get; set; }
        public BindingList<GridData> GridDataList { get; set; }
    }
}
