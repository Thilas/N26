using Newtonsoft.Json;

namespace N26.Models
{
    internal class CollectionPaging
    {
        [JsonRequired]
        public int TotalResults { get; }

        public CollectionPaging(int totalResults)
        {
            TotalResults = totalResults;
        }
    }
}
