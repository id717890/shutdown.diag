using System;

namespace ShutdownDiagnostic.Data
{
    public class OpcStatement: BaseStatement
    {
        public string ParamType { get; set; }
        public string Quality { get; set; } = string.Empty;
        public bool AllowBadQuality { get; set; }
        public bool IsModule { get; set; }
    }
}
