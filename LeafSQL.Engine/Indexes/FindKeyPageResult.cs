using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Indexes
{
    public class FindKeyPageResult
    {
        public PersistIndexPageCatalog Catalog { get; set; }
        public PersistIndexExtent Extent { get; set; }
        public PersistIndexLeaf Leaf { get; set; }
        public bool IsFullMatch { get; set; }
        public bool IsPartialMatch { get; set; }
        public int ExtentLevel { get; set; }
    }
}
