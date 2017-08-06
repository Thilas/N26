using System;
using JetBrains.Annotations;
using N26.Json;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum TokenType { Bearer }

    [Flags]
    public enum TokenScope { Read = 1 << 0, Trust = 1 << 1, Write = 1 << 2 }

    public class Token : IEquatable<Token>
    {
        [JsonProperty("access_token", Required = Required.Always)]
        public Guid AccessToken { get; }
        [JsonProperty("token_type", Required = Required.Always)]
        public TokenType TokenType { get; }
        [JsonProperty("refresh_token", Required = Required.Always)]
        public Guid RefreshToken { get; }
        [JsonProperty("expires_in", Required = Required.Always)]
        public TimeSpan ExpiresIn { get; }
        [JsonProperty("scope", Required = Required.Always), JsonConverter(typeof(StringFlagsEnumConverter))]
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
