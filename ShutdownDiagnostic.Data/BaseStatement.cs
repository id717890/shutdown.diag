using System;

namespace ShutdownDiagnostic.Data
{
    public class BaseStatement
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public object VerifyIf { get; set; }
        public bool IsVerified { get; set; }

        /// <summary>
        /// Значение тэга из конфигурации (может быть либо имя службы или тэг OPC)
        /// </summary>
        public string TagValue { get; set; }

        /// <summary>
        /// Значение которое будет считываться с сервера или хоста (при инициализации = null)
        /// </summary>
        public string Value { get; set; }
    }
}
