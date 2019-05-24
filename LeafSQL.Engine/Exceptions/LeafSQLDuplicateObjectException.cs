using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLDuplicateObjectException : LeafSQLExceptionBase
    {
        public LeafSQLDuplicateObjectException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLDuplicateObjectException(string message)
            : base($"LeafSQLDuplicateObjectException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}