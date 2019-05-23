using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLIndexDoesNotExistException : LeafSQLExceptionBase
    {
        public LeafSQLIndexDoesNotExistException()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLIndexDoesNotExistException(string message)
            : base($"LeafSQLIndexDoesNotExistException:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}