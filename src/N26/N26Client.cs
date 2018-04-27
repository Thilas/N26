using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using N26.Connection;
using N26.IoC;
using N26.Models;
using N26.Queryables;
using N26.Utilities;

namespace N26
{
    public sealed class N26Client : IClient
    {
        [NotNull]
        public static Uri ApiBaseUri
        {
            get { return _apiBaseUri; }
            set
            {
                Guard.IsNotNull(value, nameof(value));
                _apiBaseUri = value;
            }
        }

        private static Uri _apiBaseUri = new Uri("https://api.tech26.de");

        [NotNull, ItemNotNull]
        public static Task<IClient> LoginAsync([NotNull] string userName, [NotNull] string password)
        {
            var result = Bootstrapper.LoginAsync(ApiBaseUri, userName, password);
            return result;
        }

        [NotNull]
        public Token Token { get; }

        [NotNull]
        public Accounts Accounts => _accounts.Value;
        [NotNull, ItemNotNull]
        public N26Set<Address> Addresses { get; }
        [NotNull, ItemNotNull]
        public N26Set<Card> Cards { get; }
        [NotNull]
        public Me Me => _me.Value;
        [NotNull, ItemNotNull]
        public N26Set<Transaction> Transactions { get; }

        private readonly ILifetimeScope _scope;
        private readonly IConnection _connection;
        private readonly Lazy<Accounts> _accounts;
        private readonly Lazy<Me> _me;

        [IoCConstructor]
        internal N26Client([NotNull] ILifetimeScope scope, [NotNull] IConnection connection)
        {
            Guard.IsNotNull(scope, nameof(scope));
            Guard.IsNotNull(connection, nameof(connection));

            Func<IQuery, Accounts> d = query => ((IClient)this).Get<Accounts>(query);
            Func<IQuery, Me> e = query => ((IClient)this).Get<Me>(query);

            Token = connection.Token;

            Addresses = new N26Set<Address>(this);
            Cards = new N26Set<Card>(this);
            Transactions = new N26Set<Transaction>(this);

            _scope = scope;
            _connection = connection;
            _accounts = new Lazy<Accounts>(() => d(null));
            _me = new Lazy<Me>(() => e(null));
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            if (!disposedValue)
            {
                disposedValue = true;
                _scope.Dispose();
            }
        }

        #endregion

        [NotNull, ItemNotNull]
        public Task<Accounts> GetAccountsAsync() => GetAsync<Accounts>();

        [NotNull, ItemNotNull]
        public Task<IEnumerable<Address>> GetAddressesAsync() => GetEnumerableAsync<Address>();

        [NotNull, ItemNotNull]
        public Task<IEnumerable<Card>> GetCardsAsync() => GetEnumerableAsync<Card>();

        [NotNull, ItemNotNull]
        public Task<Me> GetMeAsync() => GetAsync<Me>();

        [NotNull, ItemNotNull]
        public Task<IEnumerable<Transaction>> GetTransactionsAsync() => GetEnumerableAsync<Transaction>();

        T IClient.Get<T>(IQuery query)
        {
            var task = Task.Run(async () => await GetAsync<T>(query));
            return task.Result;
        }

        IEnumerable<T> IClient.GetEnumerable<T>(IQuery query)
        {
            var task = Task.Run(async () => await GetEnumerableAsync<T>(query));
            return task.Result;
        }

        Task<T> IClient.GetAsync<T>(IQuery query)
            => _connection.GetAsync<T, T>(ApiBaseUri, query);

        Task<IEnumerable<T>> IClient.GetEnumerableAsync<T>(IQuery query)
            => _connection.GetAsync<T, IEnumerable<T>>(ApiBaseUri, query);

        private Task<T> GetAsync<T>(IQuery query = null)
            => _connection.GetAsync<T, T>(ApiBaseUri, query);

        private Task<IEnumerable<T>> GetEnumerableAsync<T>(IQuery query = null)
            => _connection.GetAsync<T, IEnumerable<T>>(ApiBaseUri, query);
    }
}
