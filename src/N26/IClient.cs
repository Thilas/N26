using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using N26.Models;
using N26.Queryables;

namespace N26
{
    public interface IClient : IDisposable
    {
        [NotNull]
        Token Token { get; }

        [NotNull]
        Accounts Accounts { get; }
        [NotNull, ItemNotNull]
        N26Set<Address> Addresses { get; }
        [NotNull, ItemNotNull]
        N26Set<Card> Cards { get; }
        [NotNull]
        Me Me { get; }
        [NotNull, ItemNotNull]
        N26Set<Transaction> Transactions { get; }

        [NotNull, ItemNotNull]
        Task<IEnumerable<T>> GetEnumerableAsync<T>([CanBeNull] IQuery query = null);

        [NotNull, ItemNotNull]
        Task<Accounts> GetAccountsAsync();

        [NotNull, ItemNotNull]
        Task<IEnumerable<Address>> GetAddressesAsync();

        [NotNull, ItemNotNull]
        Task<IEnumerable<Card>> GetCardsAsync();

        [NotNull, ItemNotNull]
        Task<Me> GetMeAsync();

        [NotNull, ItemNotNull]
        Task<IEnumerable<Transaction>> GetTransactionsAsync();

        [NotNull]
        T Get<T>([CanBeNull] IQuery query = null);

        [NotNull, ItemNotNull]
        IEnumerable<T> GetEnumerable<T>([CanBeNull] IQuery query = null);

        [NotNull, ItemNotNull]
        Task<T> GetAsync<T>([CanBeNull] IQuery query = null);
    }
}
