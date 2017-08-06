using System;
using System.Threading;
using System.Threading.Tasks;
using N26.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace N26.Json
{
    /// <summary>
    /// Converts an <see cref="Enum"/> with <see cref="FlagsAttribute"/> to and from its name string value.
    /// </summary>
    internal class StringFlagsEnumConverter : StringEnumConverter
    {
        public StringFlagsEnumConverter() : base()
        {
        }

        public StringFlagsEnumConverter(bool camelCaseText) : base(camelCaseText)
        {
        }

        public override bool CanWrite => false;

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var tempReader = new StringFlagsEnumJsonReader(reader);
            var value = base.ReadJson(tempReader, objectType, existingValue, serializer);
            return value;
        }

        private class StringFlagsEnumJsonReader : JsonReader
        {
            private readonly JsonReader _reader;

            public StringFlagsEnumJsonReader(JsonReader reader)
            {
                Guard.IsNotNull(reader, nameof(reader));
                _reader = reader;
            }

            public override object Value
            {
                get
                {
                    if (TokenType != JsonToken.String) return _reader.Value;
                    var value = _reader.Value?.ToString();
                    value = value?.Replace(' ', ',');
                    return value;
                }
            }

            public override void Close() => _reader.Close();
            public override int Depth => _reader.Depth;
            public override bool Equals(object obj) => _reader.Equals(obj);
            public override int GetHashCode() => _reader.GetHashCode();
            public override string Path => _reader.Path;
            public override bool Read() => _reader.Read();
            public override bool? ReadAsBoolean() => _reader.ReadAsBoolean();
            public override Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsBooleanAsync(cancellationToken);
            public override byte[] ReadAsBytes() => _reader.ReadAsBytes();
            public override Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsBytesAsync(cancellationToken);
            public override DateTime? ReadAsDateTime() => _reader.ReadAsDateTime();
            public override Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsDateTimeAsync(cancellationToken);
            public override DateTimeOffset? ReadAsDateTimeOffset() => _reader.ReadAsDateTimeOffset();
            public override Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsDateTimeOffsetAsync(cancellationToken);
            public override decimal? ReadAsDecimal() => _reader.ReadAsDecimal();
            public override Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsDecimalAsync(cancellationToken);
            public override double? ReadAsDouble() => _reader.ReadAsDouble();
            public override Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsDoubleAsync(cancellationToken);
            public override int? ReadAsInt32() => _reader.ReadAsInt32();
            public override Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsInt32Async(cancellationToken);
            public override string ReadAsString() => _reader.ReadAsString();
            public override Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsStringAsync(cancellationToken);
            public override Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken)) => _reader.ReadAsync(cancellationToken);
            public override JsonToken TokenType => _reader.TokenType;
            public override string ToString() => _reader.ToString();
            public override Type ValueType => _reader.ValueType;
        }
    }
}
