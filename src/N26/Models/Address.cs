using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum AddressType { Legal, Passport, Shipping }

    public class Address : N26Model<Address>
    {
        [CanBeNull]
        public string AddressLine1 { get; }
        [JsonRequired, NotNull]
        public string StreetName { get; }
        [JsonRequired, NotNull]
        public string HouseNumberBlock { get; }
        [JsonRequired, NotNull]
        public string ZipCode { get; }
        [JsonRequired, NotNull]
        public string CityName { get; }
        [JsonRequired, NotNull]
        public string CountryName { get; }
        [JsonRequired]
        public AddressType Type { get; }

        [JsonConstructor]
        internal Address(
            IN26Client n26Client,
            string addressLine1,
            string streetName,
            string houseNumberBlock,
            string zipCode,
            string cityName,
            string countryName,
            AddressType type,
            Guid id)
            : base(n26Client, id)
        {
            Guard.IsNotNullOrEmpty(streetName, nameof(streetName));
            Guard.IsNotNullOrEmpty(houseNumberBlock, nameof(houseNumberBlock));
            Guard.IsNotNullOrEmpty(zipCode, nameof(zipCode));
            Guard.IsNotNullOrEmpty(cityName, nameof(cityName));
            Guard.IsNotNullOrEmpty(countryName, nameof(countryName));
            AddressLine1 = addressLine1;
            StreetName = streetName;
            HouseNumberBlock = houseNumberBlock;
            ZipCode = zipCode;
            CityName = cityName;
            CountryName = countryName;
            Type = type;
        }

        [NotNull]
        public override string ToString() => $"{Type}, {CityName}";
    }
}
