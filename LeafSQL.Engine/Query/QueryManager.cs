using LeafSQL.Engine.Sessions;
using System;

namespace LeafSQL.Engine.Query
{
    public class QueryManager
    {
        private Core core;

        public QueryManager(Core core)
        {
            this.core = core;
        }

        public void Execute(Session session, string statement)
        {
            var preparedQuery = ParserEngine.ParseQuery(statement);
            Execute(session, preparedQuery);
        }

        public void Execute(Session session, PreparedQuery preparedQuery)
        {
            if (preparedQuery.QueryType == Constants.QueryType.Select)
            {
                core.Documents.ExecuteSelect(session, preparedQuery);
            }
        }
    }
}
