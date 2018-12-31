using System;
using System.Collections.Generic;

namespace ShutdownDiagnostic.Data
{
    public class Server
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Connectionstring { get; set; }
        public string HostName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public int Order { get; set; }
        public IEnumerable<OpcStatement> OpcStatements { get; set; }
        public IEnumerable<ServiceStatement> ServiceStatements { get; set; }
    }
}
