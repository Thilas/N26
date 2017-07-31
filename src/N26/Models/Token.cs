using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum TokenType { Bearer }

    [Flags]
    public enum TokenScope { None, Read = 1 << 0, Trust = 1 << 1, Write = 1 << 2 }

    public class Token
    {
        [JsonProperty("access_token")]
        public Guid AccessToken { get; internal set; }

        [JsonProperty("token_type")]
        public TokenType TokenType { get; internal set; }

        [JsonProperty("refresh_token")]
        public Guid RefreshToken { get; internal set; }

        [JsonProperty("expires_in")]
        internal int _expiresIn { get; set; }
        public TimeSpan ExpiresIn
        {
            get { return TimeSpanHelper.FromSeconds(_expiresIn); }
            set { _expiresIn = TimeSpanHelper.ToSeconds(value); }
        }

        public TokenScope Scope { get; set; }
    }
}
