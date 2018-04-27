using System;
using N26.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace N26.Json
{
    /// <summary>
    /// Converts an <see cref="Enum"/> to and from the name string value of <typeparamref name="T"/>.
    /// </summary>
    internal sealed class StringEnumConverter<T> : StringEnumConverter
    {
        public StringEnumConverter()
            : this(false)
        {
        }

        public StringEnumConverter(bool camelCaseText)
            : base(camelCaseText)
        {
            Guard.IsAssignableTo<Enum>(Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T), nameof(T));
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var fromValue = (T)value;

            base.WriteJson(writer, fromValue, serializer);
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
            var fromValue = base.ReadJson(reader, typeof(T), existingValue, serializer);

            if (fromValue == null)
            {
                if (!objectType.IsNullableType())
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
                }

                return null;
            }

            var toValue = Enum.ToObject(objectType, fromValue);

            return toValue;
        }
    }
}
