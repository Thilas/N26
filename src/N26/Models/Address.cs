using System;
using JetBrains.Annotations;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum AddressType { Legal, Passport, Shipping }

    public class Address : IEquatable<Address>
    {
        [CanBeNull]
        public string AddressLine1 { get; }
        [NotNull]
        public string StreetName { get; }
        [NotNull]
        public string HouseNumberBlock { get; }
        [NotNull]
        public string ZipCode { get; }
        [NotNull]
        public string CityName { get; }
        [NotNull]
        public string CountryName { get; }
        [NotNull]
        public AddressType Type { get; }
        [NotNull]
        public Guid Id { get; }

        [JsonConstructor]
        internal Address(
            string addressLine1,
            string streetName,
            string houseNumberBlock,
            string zipCode,
            string cityName,
            string countryName,
            AddressType? type,
            Guid? id)
        {
            Guard.IsNotNullNorEmpty(streetName, nameof(streetName));
            Guard.IsNotNullNorEmpty(houseNumberBlock, nameof(houseNumberBlock));
            Guard.IsNotNullNorEmpty(zipCode, nameof(zipCode));
            Guard.IsNotNullNorEmpty(cityName, nameof(cityName));
            Guard.IsNotNullNorEmpty(countryName, nameof(countryName));
            Guard.IsNotNull(type, nameof(type));
            Guard.IsNotNull(id, nameof(id));
            AddressLine1 = addressLine1;
            StreetName = streetName;
            HouseNumberBlock = houseNumberBlock;
            ZipCode = zipCode;
            CityName = cityName;
            CountryName = countryName;
            Type = type.Value;
            Id = id.Value;
        }

        [NotNull]
        public override string ToString() => $"{Type}, {CityName}";

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Address a, Address b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Address a, Address b) => !(a == b);

        public static bool Equals(Address a, Address b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Address);

        public bool Equals(Address other) => this == other;
    }
}
