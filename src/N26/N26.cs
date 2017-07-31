using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using N26.Models;
using N26.Queryables;
using N26.Queryables.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace N26
{
    public class N26
    {
        public static Uri ApiBaseUri => new Uri("https://api.tech26.de");

        private const string AccountsRelativeUri = "api/accounts";
        private const string AddressesRelativeUri = "api/addresses";
        private const string CardsRelativeUri = "api/v2/cards";
        private const string MeRelativeUri = "api/me";
        private const string TransactionsRelativeUri = "api/smrt/transactions";

        public static async Task<N26> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
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
                    var json = JObject.Parse(contentString);
                    var oldScope = json.GetValue(nameof(Models.Token.Scope), StringComparison.OrdinalIgnoreCase);
                    if (oldScope?.Type == JTokenType.String)
                    {
                        var newScope = oldScope.DeepClone();
                        newScope = newScope.ToString().Replace(' ', ',');
                        oldScope.Replace(newScope);
                    }
                    var token = json.ToObject<Token>();
                    var result = new N26(token);
                    return result;
                }
            }
        }

        public Token Token { get; }

        private readonly Func<Task<IEnumerable<Address>>> _addressesFactoryAsync;
        private readonly Func<Task<IEnumerable<Card>>> _cardsFactoryAsync;
        private readonly Func<string, Task<IEnumerable<Transaction>>> _transactionsFactoryAsync;

        public N26Set<Address> Addresses { get; }
        public N26Set<Card> Cards { get; }
        public N26Set<Transaction> Transactions { get; }

        private N26(Token token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            _addressesFactoryAsync = async () => (await GetAsync<Collection<Address>>(AddressesRelativeUri)).Data;
            _cardsFactoryAsync = async () => await GetAsync<Card[]>(CardsRelativeUri);
            _transactionsFactoryAsync = async relativeUri => await GetAsync<Transaction[]>(relativeUri);
            Addresses = new N26Set<Address>(new N26SetFactory<Address>(_addressesFactoryAsync));
            Cards = new N26Set<Card>(new N26SetFactory<Card>(_cardsFactoryAsync));
            Transactions = new N26Set<Transaction>(new TransactionFactory(_transactionsFactoryAsync, TransactionsRelativeUri));
        }

        public async Task<Accounts> GetAccountsAsync() => await GetAsync<Accounts>(AccountsRelativeUri);
        public async Task<IEnumerable<Address>> GetAddressesAsync() => await _addressesFactoryAsync();
        public async Task<IEnumerable<Card>> GetCardsAsync() => await _cardsFactoryAsync();
        public async Task<Me> GetMeAsync() => await GetAsync<Me>(MeRelativeUri);
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await _transactionsFactoryAsync(TransactionsRelativeUri);

        internal async Task<T> GetAsync<T>(string relativeUri)
        {
            if (string.IsNullOrEmpty(relativeUri)) throw new ArgumentNullException(nameof(relativeUri));
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.TokenType.ToString(), Token.AccessToken.ToString());
                using (var response = await client.GetAsync(new Uri(ApiBaseUri, relativeUri)))
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(contentString);
                    return result;
                }
            }
        }
    }
}
