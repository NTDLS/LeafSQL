using System.Collections.Generic;
using LeafSQL.Engine.Query;

namespace LeafSQL.Engine.Indexes
{
    public class IndexConditions : List<IndexCondition>
    {
        public IndexConditions(List<Condition> conditions)
        {
            foreach (Condition condition in conditions)
            {
                this.Add(new IndexCondition(condition));
            }
        }

        public IndexConditions()
        {
        }
    }
}
