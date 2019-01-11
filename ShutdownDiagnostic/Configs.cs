using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace ShutdownDiagnostic
{
    public class Configs
    {
        public static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string ConfigFileName => ConfigurationSettings.AppSettings.Get("ConfigFileName");
        public static string FileCommandName => ConfigurationSettings.AppSettings.Get("FileCommandName");

        public static string[] GoodQualityVariants()
        {
            var data = ConfigurationSettings.AppSettings.Get("GoodQualityList");
            return data.Split(',').Select(x => x.ToLower()).ToArray();
        }

        public static string[] BadQualityVariants()
        {
            var data = ConfigurationSettings.AppSettings.Get("BadQualityList");
            return data.Split(',').Select(x => x.ToLower()).ToArray(); ;
        }        
    }
}
