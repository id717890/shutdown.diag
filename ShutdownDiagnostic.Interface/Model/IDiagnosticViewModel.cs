using ShutdownDiagnostic.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShutdownDiagnostic.Interface.Model
{
    public interface IDiagnosticViewModel
    {
        //IEnumerable<Server> VerificationList { get; set; }
        BindingList<GridData> GridDataList { get; set; }
    }
}
