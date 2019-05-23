using System;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLExceptionBase : Exception
    {
        public LogSeverity Severity { get; set; }

        public LeafSQLExceptionBase()
        {
            Severity = LogSeverity.Exception;
        }

        public LeafSQLExceptionBase(string message)
            : base(message)

        {
            Severity = LogSeverity.Exception;
        }
    }
}
