using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using N26.Json;
using N26.Models;
using N26.Queryables;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Connection
{
    internal sealed class N26Connection : IConnection
    {
        private readonly IHttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings;

        private Token _token;

        [NotNull, ItemNotNull]

        public Token Token => _token ?? throw new InvalidOperationException("You must login first.");

        public N26Connection([NotNull] IHttpClient httpClient, [NotNull] ISerializerSettingsFactory serializerSettingsFactory)
        {
            Guard.IsNotNull(httpClient, nameof(httpClient));
            Guard.IsNotNull(serializerSettingsFactory, nameof(serializerSettingsFactory));
            _httpClient = httpClient;
            _serializerSettings = serializerSettingsFactory.Create();
        }

        public async Task LoginAsync([NotNull] Uri apiBaseUri, [NotNull] string userName, [NotNull] string password)
        {
            Guard.IsNotNull(apiBaseUri, nameof(apiBaseUri));
            Guard.IsNotNullOrEmpty(userName, nameof(userName));
            Guard.IsNotNullOrEmpty(password, nameof(password));
            _httpClient.SetAuthorizationHeader("Basic", "bXktdHJ1c3RlZC13ZHBDbGllbnQ6c2VjcmV0");
            var content = new Dictionary<string, string>()
                {
                    { "username", userName },
                    { "password", password },
                    { "grant_type", "password" }
                };
            var response = await _httpClient.PostAsync(GetUri<Token>(apiBaseUri), content);
            _token = JsonConvert.DeserializeObject<Token>(response, _serializerSettings);
            _httpClient.SetAuthorizationHeader(_token.TokenType.ToString(), _token.AccessToken.ToString());
        }

        [NotNull, ItemNotNull]
        public async Task<TResult> GetAsync<T, TResult>([NotNull] Uri apiBaseUri, [CanBeNull] IQuery query)
        {
            Guard.IsNotNull(apiBaseUri, nameof(apiBaseUri));
            var response = await _httpClient.GetAsync(GetUri<T>(apiBaseUri, query));
            var container = N26ModelAttribute.GetContainer<T>();
            var result = (TResult)JsonConvert.DeserializeObject(response, container, _serializerSettings);
            return result;
        }

        private Uri GetUri<T>(Uri apiBaseUri, IQuery query = null)
        {
            var relativeUri = N26ModelAttribute.GetRelativeUri<T>();
            var statement = query?.ToString();
            if (!string.IsNullOrEmpty(statement))
            {
                relativeUri = $"{relativeUri}?{statement}";
            }
            return new Uri(apiBaseUri, relativeUri);
        }
    }
}
