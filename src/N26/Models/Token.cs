using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum TokenType { Bearer }

    [Flags]
    public enum TokenScope { None = 0, Read = 1 << 0, Trust = 1 << 1, Write = 1 << 2 }

    [N26Model("oauth/token")]
    public sealed class Token : IEquatable<Token>
    {
        [JsonProperty("access_token"), JsonRequired]
        public Guid AccessToken { get; }
        [JsonProperty("token_type"), JsonRequired]
        public TokenType TokenType { get; }
        [JsonProperty("refresh_token"), JsonRequired]
        public Guid RefreshToken { get; }
        [JsonProperty("expires_in"), JsonRequired]
        public TimeSpan ExpiresIn { get; }
        [JsonProperty("scope"), JsonRequired]
        public TokenScope Scope { get; }

        [JsonConstructor]
        internal Token(Guid accessToken, TokenType tokenType, Guid refreshToken, TimeSpan expiresIn, TokenScope scope)
        {
            AccessToken = accessToken;
            TokenType = tokenType;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            Scope = scope;
        }

        [NotNull]
        public override string ToString() => $"{TokenType}, {AccessToken}";

        public override int GetHashCode() => AccessToken.GetHashCode();
        public static bool Equals(Token left, Token right) => left?.AccessToken == right?.AccessToken;
        public override bool Equals(object obj) => Equals(this, obj as Token);
        public bool Equals(Token other) => Equals(this, other);
        public static bool operator ==(Token left, Token right) => Equals(left, right);
        public static bool operator !=(Token left, Token right) => !Equals(left, right);
    }
}
