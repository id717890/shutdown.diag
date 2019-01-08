using System;
using System.ComponentModel;

namespace ShutdownDiagnostic.Data
{
    public enum ParameterStatement
    {
        Service = 1,
        OpcTag = 2
    }


    public class GridData: INotifyPropertyChanged
    {
        public Guid ServerId { get; set; }
        public string ServerCaption { get; set; }
        public string Connectionstring { get; set; }
        public string HostName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public int Order { get; set; }

        public Guid StatementId { get; set; }
        public string StatementCaption { get; set; }
        public object VerifyIf { get; set; }
        public bool IsVerified { get; set; }
        public ParameterStatement ParameterStatement { get; set; }

        /// <summary>
        /// Значение тэга из конфигурации (может быть либо имя службы или тэг OPC)
        /// </summary>
        public string TagValue { get; set; }

        /// <summary>
        /// Значение которое будет считываться с сервера или хоста (при инициализации = null)
        /// </summary>
        string value;
        public string Value { get { return value; } set { this.value = value; NotifyChanged("Value"); } }

        string quality = string.Empty;
        public string Quality { get { return quality; } set { quality = value; NotifyChanged("Quality"); } }

        public string ParamType { get; set; }

        public bool AllowBadQuality { get; set; }

        void NotifyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
