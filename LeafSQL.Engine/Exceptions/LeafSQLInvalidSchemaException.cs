using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLInvalidSchemaException : LeafSQLExceptionBase
    {
        public LeafSQLInvalidSchemaException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLInvalidSchemaException(string message)
            : base($"LeafSQLInvalidSchemaException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}