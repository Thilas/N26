using JetBrains.Annotations;
using Newtonsoft.Json;

namespace N26.Json
{
    internal interface ISerializerSettingsFactory
    {
        [NotNull]
        JsonSerializerSettings Create();
    }
}
