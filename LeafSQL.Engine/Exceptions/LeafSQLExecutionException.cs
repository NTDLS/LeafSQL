using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLExecutionException : LeafSQLExceptionBase
    {
        public LeafSQLExecutionException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLExecutionException(string message)
            : base($"LeafSQLExecutionException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}