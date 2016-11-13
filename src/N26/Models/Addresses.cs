using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Addresses
    {
        public AddressesPaging paging { get; set; }
        public List<Address> data { get; set; }
    }
}
