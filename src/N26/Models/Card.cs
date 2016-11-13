using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Card
    {
        public string maskedPan { get; set; }
        public long expirationDate { get; set; }
        public string cardType { get; set; }
        public bool exceetExpressCardDelivery { get; set; }
        public bool exceetExpressCardDeliveryEmailSent { get; set; }
        public string n26Status { get; set; }
        public long pinDefined { get; set; }
        public long cardActivated { get; set; }
        public string usernameOnCard { get; set; }
        public string id { get; set; }
    }
}
