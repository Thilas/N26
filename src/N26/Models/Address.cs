using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum AddressType { Legal, Passport, Shipping }

    [N26Model("api/addresses", typeof(Collection<>))]
    public sealed class Address : N26Model<Address>
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
        [JsonRequired]
        public Guid UserId { get; }

        [JsonConstructor]
        internal Address(
            [NotNull] IClient client,
            [CanBeNull] string addressLine1,
            [NotNull] string streetName,
            [NotNull] string houseNumberBlock,
            [NotNull] string zipCode,
            [NotNull] string cityName,
            [NotNull] string countryName,
            AddressType type,
            Guid userId,
            Guid id)
            : base(client, id)
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
            UserId = userId;
        }

        [NotNull]
        public override string ToString() => $"{Type}, {CityName}";
    }
}
