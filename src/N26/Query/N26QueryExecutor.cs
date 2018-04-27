using System.Collections.Generic;
using System.Linq;
using N26.Utilities;
using Remotion.Linq;

namespace N26.Queryables
{
    internal class N26QueryExecutor : IQueryExecutor
    {
        private readonly IClient _client;

        public N26QueryExecutor(IClient client)
        {
            Guard.IsNotNull(client, nameof(client));
            _client = client;
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return ExecuteCollection<T>(queryModel).Single();
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            return returnDefaultWhenEmpty ? ExecuteCollection<T>(queryModel).SingleOrDefault() : ExecuteCollection<T>(queryModel).Single();
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            var query = N26QueryModelVisitor.GenerateQuery(queryModel);
            var result = _client.GetEnumerable<T>(query);
            return result;
        }
    }
}
