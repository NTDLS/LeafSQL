using LeafSQL.Engine.Query;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Indexes
{
    public class IndexKeyMatch : Condition
    {
        public bool Handled { get; set; }

        public IndexKeyMatch()
        {
        }

        public IndexKeyMatch(string key, ConditionQualifier conditionQualifier, string value)
        {
            this.Key = key.ToLower();
            this.Value = value.ToLower();
            this.ConditionQualifier = conditionQualifier;
        }

        public IndexKeyMatch(Condition condition)
        {
            this.Key = condition.Key.ToLower();
            this.Value = condition.Value.ToLower();
            this.ConditionQualifier = condition.ConditionQualifier;
        }
    }
}
