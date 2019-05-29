using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.UI
{
    public static class Utility
    {
        private static int nextFileName = 1;
        public static string GetNextFileName()
        {
            return ("New Query " + (nextFileName++) + ".lsql");
        }
    }
}
