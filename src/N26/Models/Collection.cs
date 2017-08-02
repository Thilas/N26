using N26.Helpers;

namespace N26.Models
{
    internal class Collection<T>
    {
        public T[] Data { get; }

        public Collection(T[] data)
        {
            Guard.IsNotNull(data, nameof(data));
            Data = data;
        }
    }
}
