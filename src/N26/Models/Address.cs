using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Address
    {
        public string addressLine1 { get; set; }
        public string streetName { get; set; }
        public string houseNumberBlock { get; set; }
        public string zipCode { get; set; }
        public string cityName { get; set; }
        public string countryName { get; set; }
        public string type { get; set; }
        public string id { get; set; }
    }
}
