using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum Gender { Male, Female }

    public class Me
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string KycFirstName { get; set; }

        public string KycLastName { get; set; }

        public string Title { get; set; }

        public Gender Gender { get; set; }

        [JsonProperty("birthDate")]
        private long _birthDate;
        public DateTime BirthDate
        {
            get { return DateTimeHelper.FromJsDate(_birthDate); }
            set { _birthDate = DateTimeHelper.ToJsDate(value); }
        }

        public bool SignupCompleted { get; set; }

        public string Nationality { get; set; }

        public string MobilePhoneNumber { get; set; }

        public Guid ShadowUserId { get; set; }

        public bool TransferWiseTermsAccepted { get; set; }

        public string IdNowToken { get; set; }
    }
}