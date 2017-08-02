using System;
using JetBrains.Annotations;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum Gender { Male, Female }

    public class Me : IEquatable<Me>
    {
        [NotNull]
        public Guid Id { get; }
        [NotNull]
        public string Email { get; }
        [NotNull]
        public string FirstName { get; }
        [NotNull]
        public string LastName { get; }
        [NotNull]
        public string KycFirstName { get; }
        [NotNull]
        public string KycLastName { get; }
        [CanBeNull]
        public string Title { get; }
        [NotNull]
        public Gender Gender { get; }
        [NotNull]
        public DateTime BirthDate { get; }
        [NotNull]
        public bool SignupCompleted { get; }
        [NotNull]
        public string Nationality { get; }
        [NotNull]
        public string MobilePhoneNumber { get; }
        [NotNull]
        public Guid ShadowUserId { get; }
        [NotNull]
        public bool TransferWiseTermsAccepted { get; }
        [NotNull]
        public string IdNowToken { get; }

        [JsonConstructor]
        internal Me(
            Guid? id, string email, string firstName, string lastName, string kycFirstName, string kycLastName, string title,
            Gender? gender, long? birthDate, bool? signupCompleted, string nationality, string mobilePhoneNumber,
            Guid? shadowUserId, bool? transferWiseTermsAccepted, string idNowToken)
            : this(
                  id, email, firstName, lastName, kycFirstName, kycLastName, title,
                  gender, DateTimeHelper.FromJsDate(birthDate), signupCompleted, nationality, mobilePhoneNumber,
                  shadowUserId, transferWiseTermsAccepted, idNowToken)
        {
        }

        private Me(
            Guid? id,
            string email,
            string firstName,
            string lastName,
            string kycFirstName,
            string kycLastName,
            string title,
            Gender? gender,
            DateTime? birthDate,
            bool? signupCompleted,
            string nationality,
            string mobilePhoneNumber,
            Guid? shadowUserId,
            bool? transferWiseTermsAccepted,
            string idNowToken)
        {
            Guard.IsNotNull(id, nameof(id));
            Guard.IsNotNullOrEmpty(email, nameof(email));
            Guard.IsNotNullOrEmpty(firstName, nameof(firstName));
            Guard.IsNotNullOrEmpty(lastName, nameof(lastName));
            Guard.IsNotNullOrEmpty(kycFirstName, nameof(kycFirstName));
            Guard.IsNotNullOrEmpty(kycLastName, nameof(kycLastName));
            Guard.IsNotNull(gender, nameof(gender));
            Guard.IsNotNull(birthDate, nameof(birthDate));
            Guard.IsNotNull(signupCompleted, nameof(signupCompleted));
            Guard.IsNotNullOrEmpty(nationality, nameof(nationality));
            Guard.IsNotNullOrEmpty(mobilePhoneNumber, nameof(mobilePhoneNumber));
            Guard.IsNotNull(shadowUserId, nameof(shadowUserId));
            Guard.IsNotNull(transferWiseTermsAccepted, nameof(transferWiseTermsAccepted));
            Guard.IsNotNullOrEmpty(idNowToken, nameof(idNowToken));
            Id = id.Value;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            KycFirstName = kycFirstName;
            KycLastName = kycLastName;
            Title = title;
            Gender = gender.Value;
            BirthDate = birthDate.Value;
            SignupCompleted = signupCompleted.Value;
            Nationality = nationality;
            MobilePhoneNumber = mobilePhoneNumber;
            ShadowUserId = shadowUserId.Value;
            TransferWiseTermsAccepted = transferWiseTermsAccepted.Value;
            IdNowToken = idNowToken;
        }

        [NotNull]
        public override string ToString() => $"{FirstName} {LastName}, {Email}";

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Me a, Me b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Me a, Me b) => !(a == b);

        public static bool Equals(Me a, Me b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Me);

        public bool Equals(Me other) => this == other;
    }
}
