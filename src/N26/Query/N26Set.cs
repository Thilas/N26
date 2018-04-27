using System.Linq;
using System.Linq.Expressions;
using N26.Models;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace N26.Queryables
{
    public class N26Set<T> : QueryableBase<T>
        where T : N26Model<T>
    {
        internal N26Set(IClient client)
            : base(QueryParser.CreateDefault(), new N26QueryExecutor(client))
        {
        }

        internal N26Set(IQueryProvider provider)
            : base(provider)
        {
        }

        internal N26Set(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }
    }
}
