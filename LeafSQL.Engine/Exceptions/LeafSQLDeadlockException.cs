using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLDeadlockException : LeafSQLExceptionBase
    {
        public LeafSQLDeadlockException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLDeadlockException(string message)
            : base($"LeafSQLDeadlockException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}