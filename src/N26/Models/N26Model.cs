using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    public abstract class N26Model<T> : IEquatable<N26Model<T>>
        where T : N26Model<T>
    {
        /// <summary>
        /// Gets the N26 client.
        /// </summary>
        /// <remarks>
        /// <see cref="Client" /> is decorated with <see cref="JsonPropertyAttribute" />
        /// so that it gets its value from <see cref="Newtonsoft.Json" /> through <see cref="Autofac" />.
        /// </remarks>
        [JsonProperty, NotNull]
        protected IClient Client { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonRequired]
        public Guid Id { get; }

        protected N26Model([NotNull] IClient client, Guid id)
        {
            Guard.IsNotNull(client, nameof(client));
            Client = client;
            Id = id;
        }

        [NotNull]
        public override string ToString() => Id.ToString();

        public override int GetHashCode() => Id.GetHashCode();
        public static bool Equals(N26Model<T> left, N26Model<T> right) => left?.Id == right?.Id;
        public override bool Equals(object obj) => Equals(this, obj as N26Model<T>);
        public bool Equals(N26Model<T> other) => Equals(this, other);
        public static bool operator ==(N26Model<T> left, N26Model<T> right) => Equals(left, right);
        public static bool operator !=(N26Model<T> left, N26Model<T> right) => !Equals(left, right);
    }
}
