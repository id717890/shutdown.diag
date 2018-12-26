using System;
using System.Collections.Generic;

namespace ShutdownDiagnostic.Data
{
    public class Server
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Connectionstring { get; set; }
        public int Order { get; set; }
        public IEnumerable<Statement> Statements { get; set; }
    }
}
