using System;

namespace ShutdownDiagnostic.Data
{
    public class Statement
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string FullTag { get; set; }
        public object VerifyIf { get; set; }
        public string ParamType { get; set; }
        public string Value { get; set; }
        public string Quality { get; set; }
        public bool AllowBadQuality { get; set; }
    }
}
