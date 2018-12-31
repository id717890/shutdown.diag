using System;

namespace ShutdownDiagnostic.Data
{
    public class OpcStatement: BaseStatement
    {
        public string ParamType { get; set; }
        public string Quality { get; set; }
        public bool AllowBadQuality { get; set; }
    }
}
