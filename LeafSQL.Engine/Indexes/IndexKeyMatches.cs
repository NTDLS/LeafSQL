using System.Collections.Generic;
using LeafSQL.Engine.Query;

namespace LeafSQL.Engine.Indexes
{
    public class IndexKeyMatches : List<IndexKeyMatch>
    {
        public IndexKeyMatches(List<Condition> conditions)
        {
            foreach (Condition condition in conditions)
            {
                this.Add(new IndexKeyMatch(condition));
            }
        }

        public IndexKeyMatches()
        {
        }
    }
}
