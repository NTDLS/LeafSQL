using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLSchemaDoesNotExistException : LeafSQLExceptionBase
    {
        public LeafSQLSchemaDoesNotExistException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLSchemaDoesNotExistException(string message)
            : base($"LeafSQLSchemaDoesNotExistException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}