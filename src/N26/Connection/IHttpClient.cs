using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N26.Connection
{
    public interface IHttpClient : IDisposable
    {
        void SetAuthorizationHeader(string scheme, string parameter);

        Task<string> GetAsync(Uri requestUri);

        Task<string> PostAsync(Uri requestUri, IEnumerable<KeyValuePair<string, string>> content);
    }
}
