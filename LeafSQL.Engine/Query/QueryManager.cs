using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Sessions;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Engine.Query
{
    public class QueryManager : CoreManagementBase
    {
        public QueryManager(Core core) : base(core)
        {
        }

        public QueryResult Execute(Session session, string statement)
        {
            var preparedQuery = ParserEngine.ParseQuery(statement);
            return Execute(session, preparedQuery);
        }

        public QueryResult Execute(Session session, PreparedQuery preparedQuery)
        {
            if (preparedQuery.QueryType == Constants.QueryType.Select)
            {
                return core.Documents.ExecuteSelect(session, preparedQuery);
            }

            throw new LeafSQLExecutionException("The query type has not been implemented.");
        }
    }
}
