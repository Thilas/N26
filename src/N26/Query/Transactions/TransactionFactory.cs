using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using N26.Utilities;
using N26.Models;

namespace N26.Queryables.Transactions
{
    internal class TransactionFactory
    {
        public TransactionFactory(Func<string, Task<IEnumerable<Transaction>>> factoryAsync, string relativeUri)
        {
        }

        private static string GetRelativeUri(string relativeUri, Expression expression)
        {
            var transaction = new TransactionVisitor(expression);
            var parameters = new List<string>();
            if (!string.IsNullOrEmpty(transaction.Keyword)) parameters.Add($"textFilter={WebUtility.UrlEncode(transaction.Keyword)}");
            if (transaction.DateFrom.HasValue) parameters.Add($"from={DateTimeUtils.ToN26DateTime(transaction.DateFrom.Value)}");
            if (transaction.DateTo.HasValue) parameters.Add($"to={DateTimeUtils.ToN26DateTime(transaction.DateTo.Value)}");
            if (transaction.Category.HasValue) parameters.Add($"categories={transaction.Category.Value}");
            if (transaction.Limit.HasValue) parameters.Add($"limit={transaction.Limit.Value}");
            if (transaction.LastId.HasValue) parameters.Add($"lastId={transaction.LastId.Value}");
            return parameters.Any() ? $"{relativeUri}?{string.Join("&", parameters)}" : relativeUri;
        }
    }
}
