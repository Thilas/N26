using System;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Json
{
    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to and from a N26 timespan.
    /// </summary>
    internal sealed class N26TimeSpanConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///   <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
		public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Guard.IsNotNull(writer, nameof(writer));

            var timeSpan = (TimeSpan)value;
            var seconds = TimeSpanUtils.ToN26TimeSpan(timeSpan);

            writer.WriteValue(seconds);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing property value of the JSON that is being converted.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="JsonSerializationException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Guard.IsNotNull(reader, nameof(reader));

            if (reader.TokenType == JsonToken.Null)
            {
                if (!objectType.IsNullableType())
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
                }

                return null;
            }

            if (reader.TokenType != JsonToken.Integer)
            {
                throw new JsonSerializationException($"Unexpected token parsing timespan. Expected Integer, got {reader.TokenType}.");
            }

            var seconds = (long)reader.Value;
            var timeSpan = TimeSpanUtils.FromN26TimeSpan(seconds);

            return timeSpan;
        }
    }
}
