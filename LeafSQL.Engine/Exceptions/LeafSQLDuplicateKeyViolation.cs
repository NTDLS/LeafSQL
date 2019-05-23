using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Exceptions
{
    public class LeafSQLDuplicateKeyViolation : LeafSQLExceptionBase
    {
        public LeafSQLDuplicateKeyViolation()
        {
            Severity = LogSeverity.Warning;
        }

        public LeafSQLDuplicateKeyViolation(string message)
            : base($"LeafSQLDuplicateKeyViolation:{message}")
        {
            Severity = LogSeverity.Warning;
        }
    }
}