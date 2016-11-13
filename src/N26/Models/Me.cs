using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Me
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string kycFirstName { get; set; }
        public string kycLastName { get; set; }
        public string title { get; set; }
        public string gender { get; set; }
        public long birthDate { get; set; }
        public string passwordHash { get; set; }
        public bool signupCompleted { get; set; }
        public string nationality { get; set; }
        public string mobilePhoneNumber { get; set; }
        public bool transferWiseTermsAccepted { get; set; }
        public string shadowID { get; set; }
        public string cardName { get; set; }
        public string id { get; set; }
    }
}