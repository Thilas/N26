using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace N26.Connection
{
    internal sealed class N26HttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public void SetAuthorizationHeader(string scheme, string parameter)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        }

        public async Task<string> GetAsync(Uri requestUri)
        {
            using (var response = await _httpClient.GetAsync(requestUri))
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        public async Task<string> PostAsync(Uri requestUri, IEnumerable<KeyValuePair<string, string>> content)
        {
            using (var response = await _httpClient.PostAsync(requestUri, new FormUrlEncodedContent(content)))
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
