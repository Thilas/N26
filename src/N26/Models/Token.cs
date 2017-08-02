using System;
using JetBrains.Annotations;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum TokenType { Bearer }

    [Flags]
    public enum TokenScope { Read = 1 << 0, Trust = 1 << 1, Write = 1 << 2 }

    public class Token : IEquatable<Token>
    {
        [NotNull]
        public Guid AccessToken { get; }
        [NotNull]
        public TokenType TokenType { get; }
        [NotNull]
        public Guid RefreshToken { get; }
        [NotNull]
        public TimeSpan ExpiresIn { get; }
        [NotNull]
        public TokenScope Scope { get; }

        [JsonConstructor]
        internal Token(Guid? access_token, TokenType? token_type, Guid? refresh_token, int? expires_in, TokenScope? scope)
        {
            Guard.IsNotNull(access_token, nameof(access_token));
            Guard.IsNotNull(token_type, nameof(token_type));
            Guard.IsNotNull(refresh_token, nameof(refresh_token));
            Guard.IsNotNull(expires_in, nameof(expires_in));
            Guard.IsNotNull(scope, nameof(scope));
            AccessToken = access_token.Value;
            TokenType = token_type.Value;
            RefreshToken = refresh_token.Value;
            ExpiresIn = TimeSpanHelper.FromSeconds(expires_in).Value;
            Scope = scope.Value;
        }

        [NotNull]
        public override string ToString() => $"{TokenType}, {AccessToken}";

        public override int GetHashCode() => AccessToken.GetHashCode();

        public static bool operator ==(Token a, Token b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.AccessToken == b.AccessToken;
        }

        public static bool operator !=(Token a, Token b) => !(a == b);

        public static bool Equals(Token a, Token b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Token);

        public bool Equals(Token other) => this == other;
    }
}
