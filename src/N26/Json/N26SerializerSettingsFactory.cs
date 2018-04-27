using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace N26.Json
{
    internal sealed class N26SerializerSettingsFactory : ISerializerSettingsFactory
    {
        private readonly IContractResolver _contractResolver;
        private readonly IList<JsonConverter> _converters;

        public N26SerializerSettingsFactory([NotNull] IContractResolver contractResolver, [NotNull, ItemNotNull] IEnumerable<JsonConverter> converters)
        {
            Guard.IsNotNull(contractResolver, nameof(contractResolver));
            Guard.IsNotNull(converters, nameof(converters));
            _contractResolver = contractResolver;
            _converters = converters.ToList();
        }

        [NotNull]
        public JsonSerializerSettings Create()
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = _contractResolver,
                Converters = _converters,
                DefaultValueHandling = DefaultValueHandling.Populate,
                FloatParseHandling = FloatParseHandling.Decimal
            };
#if DEBUG
            // in order to catch missing members while debugging, throw an error
            settings.MissingMemberHandling = MissingMemberHandling.Error;
#endif
            return settings;
        }
    }
}
