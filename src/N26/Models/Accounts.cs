using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Accounts
    {
        public string status { get; set; }
        public float availableBalance { get; set; }
        public float usableBalance { get; set; }
        public float bankBalance { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
        public string bankName { get; set; }
        public bool seized { get; set; }
        public string id { get; set; }
    }
}
