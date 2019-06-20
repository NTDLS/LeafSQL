using LeafSQL.Engine.Query;
using System;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Indexes
{
    public class IndexCondition : Condition
    {
        public bool Handled { get; set; }

        public IndexCondition()
        {
        }

        public IndexCondition(string key, ConditionQualifier conditionQualifier, string value, Guid conditonId)
        {
            this.Id = conditonId;
            this.Key = key.ToLower();
            this.Value = value.ToLower();
            this.ConditionQualifier = conditionQualifier;
        }

        public IndexCondition(Condition condition)
        {
            Id = condition.Id;
            Key = condition.Key;
            IsKeyConstant = condition.IsKeyConstant;
            Value = condition.Value;
            IsValueConstant = condition.IsValueConstant;
            ConditionQualifier = condition.ConditionQualifier;
            ConditionType = condition.ConditionType;
        }
    }
}
