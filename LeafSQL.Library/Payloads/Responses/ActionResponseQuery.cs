using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseQuery : ActionResponseBase
    {
        public QueryResult Result { get; set; }

        public ActionResponseQuery()
        {
            Result = new QueryResult();
        }
    }
}
