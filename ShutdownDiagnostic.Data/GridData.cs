using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ShutdownDiagnostic.Data
{
    public enum ParameterStatement
    {
        Service = 1,
        OpcTag = 2
    }

    //public static string GetDescription<T>(this T e) where T : IConvertible
    //{
    //    if (e is Enum)
    //    {
    //        Type type = e.GetType();
    //        Array values = System.Enum.GetValues(type);
    //        foreach (int val in values)
    //        {
    //            if (val == e.ToInt32(CultureInfo.InvariantCulture))
    //            {
    //                var memInfo = type.GetMember(type.GetEnumName(val));
    //                var descriptionAttribute = memInfo[0]
    //                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
    //                    .FirstOrDefault() as DescriptionAttribute;
    //                if (descriptionAttribute != null)
    //                {
    //                    return descriptionAttribute.Description;
    //                }
    //            }
    //        }
    //    }
    //    return null;
    //}


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
        public ParameterStatement ParameterStatement { get; set; }

        /// <summary>
        /// Значение тэга из конфигурации (может быть либо имя службы или тэг OPC)
        /// </summary>
        public string TagValue { get; set; }

        bool isVerified = false;
        public bool IsVerified { get { return isVerified; } set { isVerified = value; NotifyChanged("IsVerified"); } }

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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
