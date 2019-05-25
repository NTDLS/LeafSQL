using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLInvalidUsernameOrPassword : LeafSQLExceptionBase
    {
        public LeafSQLInvalidUsernameOrPassword()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLInvalidUsernameOrPassword(string message)
            : base($"LeafSQLInvalidUsernameOrPassword:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}