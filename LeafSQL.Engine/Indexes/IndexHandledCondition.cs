using LeafSQL.Engine.Query;

namespace LeafSQL.Engine.Indexes
{
    public class IndexHandledCondition : Condition
    {
        public int IndexTreeLevel { get; set; }

        public IndexHandledCondition(IndexCondition condition, int indexTreeLevel)
        {
            Id = condition.Id;
            Key = condition.Key;
            IsKeyConstant = condition.IsKeyConstant;
            Value = condition.Value;
            IsValueConstant = condition.IsValueConstant;
            ConditionQualifier = condition.ConditionQualifier;
            ConditionType = condition.ConditionType;

            IndexTreeLevel = indexTreeLevel;
        }
    }
}
