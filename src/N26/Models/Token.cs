using System;
using Newtonsoft.Json;

namespace N26.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }
        [JsonProperty("expires_in")]
        internal int _expiresIn { get; set; }
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(_expiresIn);
        public string Scope { get; set; }
    }
}
