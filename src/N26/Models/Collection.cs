using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    internal class Collection<T>
    {
        [JsonRequired]
        public CollectionPaging Paging { get; }

        [JsonRequired]
        public T[] Data { get; }

        public Collection(CollectionPaging paging, T[] data)
        {
            Guard.IsNotNull(paging, nameof(paging));
            Guard.IsNotNull(data, nameof(data));
            Paging = paging;
            Data = data;
        }

        public class CollectionPaging
        {
            [JsonRequired]
            public int TotalResults { get; }

            public CollectionPaging(int totalResults)
            {
                TotalResults = totalResults;
            }
        }
    }
}
