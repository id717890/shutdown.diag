using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownDiagnostic
{
    public class Configs
    {
        public static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string ConfigFileName => ConfigurationSettings.AppSettings.Get("ConfigFileName");
    }
}
