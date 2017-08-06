using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum Gender { Male, Female }

    public class Me : N26Model<Me>
    {
        [JsonRequired, NotNull]
        public string Email { get; }
        [JsonRequired, NotNull]
        public string FirstName { get; }
        [JsonRequired, NotNull]
        public string LastName { get; }
        [JsonRequired, NotNull]
        public string KycFirstName { get; }
        [JsonRequired, NotNull]
        public string KycLastName { get; }
        [CanBeNull]
        public string Title { get; }
        [JsonRequired]
        public Gender Gender { get; }
        [JsonRequired]
        public DateTime BirthDate { get; }
        [JsonRequired]
        public bool SignupCompleted { get; }
        [JsonRequired, NotNull]
        public string Nationality { get; }
        [JsonRequired, NotNull]
        public string MobilePhoneNumber { get; }
        [JsonRequired]
        public Guid ShadowUserId { get; }
        [JsonRequired]
        public bool TransferWiseTermsAccepted { get; }
        [JsonRequired, NotNull]
        public string IdNowToken { get; }

        [JsonConstructor]
        internal Me(
            IN26Client n26Client,
            Guid id,
            string email,
            string firstName,
            string lastName,
            string kycFirstName,
            string kycLastName,
            string title,
            Gender gender,
            DateTime birthDate,
            bool signupCompleted,
            string nationality,
            string mobilePhoneNumber,
            Guid shadowUserId,
            bool transferWiseTermsAccepted,
            string idNowToken)
            : base(n26Client, id)
        {
            Guard.IsNotNullOrEmpty(email, nameof(email));
            Guard.IsNotNullOrEmpty(firstName, nameof(firstName));
            Guard.IsNotNullOrEmpty(lastName, nameof(lastName));
            Guard.IsNotNullOrEmpty(kycFirstName, nameof(kycFirstName));
            Guard.IsNotNullOrEmpty(kycLastName, nameof(kycLastName));
            Guard.IsNotNullOrEmpty(nationality, nameof(nationality));
            Guard.IsNotNullOrEmpty(mobilePhoneNumber, nameof(mobilePhoneNumber));
            Guard.IsNotNullOrEmpty(idNowToken, nameof(idNowToken));
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            KycFirstName = kycFirstName;
            KycLastName = kycLastName;
            Title = title;
            Gender = gender;
            BirthDate = birthDate;
            SignupCompleted = signupCompleted;
            Nationality = nationality;
            MobilePhoneNumber = mobilePhoneNumber;
            ShadowUserId = shadowUserId;
            TransferWiseTermsAccepted = transferWiseTermsAccepted;
            IdNowToken = idNowToken;
        }

        [NotNull]
        public override string ToString() => $"{FirstName} {LastName}, {Email}";
    }
}
