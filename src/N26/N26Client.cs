using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using N26.Json;
using N26.Models;
using N26.Queryables;
using N26.Queryables.Transactions;
using N26.Utilities;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("N26.Tests")]

namespace N26
{
    public class N26Client : IN26Client
    {
        [NotNull]
        public static Uri ApiBaseUri => new Uri("https://api.tech26.de");

        private const string AccountsRelativeUri = "api/accounts";
        private const string AddressesRelativeUri = "api/addresses";
        private const string CardsRelativeUri = "api/v2/cards";
        private const string MeRelativeUri = "api/me";
        private const string TransactionsRelativeUri = "api/smrt/transactions";

        [NotNull]
        public static async Task<N26Client> LoginAsync(string userName, string password)
        {
            const string Bearer = "bXktdHJ1c3RlZC13ZHBDbGllbnQ6c2VjcmV0";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Bearer);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "username", userName },
                    { "password", password },
                    { "grant_type", "password" }
                });
                using (var response = await client.PostAsync(new Uri(ApiBaseUri, "oauth/token"), content))
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<Token>(contentString, GetJsonSerializerSettings());
                    var result = new N26Client(token);
                    return result;
                }
            }
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                Converters = { new N26DateTimeConverter(), new N26TimeSpanConverter() },
                DefaultValueHandling = DefaultValueHandling.Populate,
                FloatParseHandling = FloatParseHandling.Decimal
            };
#if DEBUG
            // in order to catch missing members, throw an error in debug only
            settings.MissingMemberHandling = MissingMemberHandling.Error;
#endif
            return settings;
        }

        private readonly IContainer _container;
        private readonly Func<Task<IEnumerable<Address>>> _addressesFactoryAsync;
        private readonly Func<Task<IEnumerable<Card>>> _cardsFactoryAsync;
        private readonly Func<string, Task<IEnumerable<Transaction>>> _transactionsFactoryAsync;

        [NotNull]
        public Token Token { get; }

        [NotNull, ItemNotNull]
        public N26Set<Address> Addresses { get; }
        [NotNull, ItemNotNull]
        public N26Set<Card> Cards { get; }
        [NotNull, ItemNotNull]
        public N26Set<Transaction> Transactions { get; }

        private N26Client(Token token)
        {
            Guard.IsNotNull(token, nameof(token));

            _container = GetContainer(this);
            _addressesFactoryAsync = async () => (await GetAsync<Collection<Address>>(AddressesRelativeUri)).Data;
            _cardsFactoryAsync = async () => await GetAsync<Card[]>(CardsRelativeUri);
            _transactionsFactoryAsync = async relativeUri => await GetAsync<Transaction[]>(relativeUri);

            Token = token;
            Addresses = new N26Set<Address>(new N26SetFactory<Address>(_addressesFactoryAsync));
            Cards = new N26Set<Card>(new N26SetFactory<Card>(_cardsFactoryAsync));
            Transactions = new N26Set<Transaction>(new TransactionFactory(_transactionsFactoryAsync, TransactionsRelativeUri));
        }

        private static IContainer GetContainer(IN26Client n26Client)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(n26Client).As<IN26Client>().ExternallyOwned();
            return builder.Build();
        }

        [NotNull]
        public async Task<Accounts> GetAccountsAsync() => await GetAsync<Accounts>(AccountsRelativeUri);
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<Address>> GetAddressesAsync() => await _addressesFactoryAsync();
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<Card>> GetCardsAsync() => await _cardsFactoryAsync();
        [NotNull]
        public async Task<Me> GetMeAsync() => await GetAsync<Me>(MeRelativeUri);
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await _transactionsFactoryAsync(TransactionsRelativeUri);

        internal async Task<T> GetAsync<T>(string relativeUri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.TokenType.ToString(), Token.AccessToken.ToString());
#if DEBUG
                if (string.Equals(relativeUri, TransactionsRelativeUri, StringComparison.OrdinalIgnoreCase))
                {
                    relativeUri = $"{relativeUri}?limit=1000";
                }
#endif
                using (var response = await client.GetAsync(new Uri(ApiBaseUri, relativeUri)))
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(contentString, GetAutofacJsonSerializerSettings());
                    return result;
                }
            }
        }

        private JsonSerializerSettings GetAutofacJsonSerializerSettings()
        {
            var settings = GetJsonSerializerSettings();
            settings.ContractResolver = new AutofacContractResolver(_container);
            return settings;
        }
    }
}
