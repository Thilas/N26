using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using N26.Models;
using N26.Queryables;

namespace N26.Connection
{
    internal interface IConnection
    {
        [NotNull]
        Token Token { get; }

        [NotNull, ItemNotNull]
        Task LoginAsync([NotNull] Uri apiBaseUri, [NotNull] string userName, [NotNull] string password);

        [NotNull, ItemNotNull]
        Task<TResult> GetAsync<T, TResult>([NotNull] Uri apiBaseUri, [CanBeNull] IQuery query);
    }
}
