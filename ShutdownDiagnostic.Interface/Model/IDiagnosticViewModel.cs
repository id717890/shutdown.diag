using ShutdownDiagnostic.Data;
using System.Collections.Generic;

namespace ShutdownDiagnostic.Interface.Model
{
    public interface IDiagnosticViewModel
    {
        //IEnumerable<Server> VerificationList { get; set; }
        IEnumerable<GridData> GridDataList { get; set; }
    }
}
