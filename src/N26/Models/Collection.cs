using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    [JsonObject]
    internal sealed partial class Collection<T> : IEnumerable<T>
    {
        [JsonRequired, NotNull]
        public CollectionPaging Paging { get; }

        [JsonRequired, NotNull, ItemNotNull]
        public ImmutableList<T> Data { get; }

        public Collection([NotNull] CollectionPaging paging, [NotNull, ItemNotNull] ImmutableList<T> data)
        {
            Guard.IsNotNull(paging, nameof(paging));
            Guard.IsNotNull(data, nameof(data));
            Paging = paging;
            Data = data;
        }

        [NotNull, ItemNotNull]
        public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

        [NotNull, ItemNotNull]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
