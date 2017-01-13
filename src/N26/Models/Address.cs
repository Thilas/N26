namespace N26.Models
{
    public enum AddressType { Passport, Shipping }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string StreetName { get; set; }
        public string HouseNumberBlock { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public AddressType Type { get; set; }
        public string Id { get; set; }
    }
}
