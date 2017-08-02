using System;
using JetBrains.Annotations;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public class Accounts : IEquatable<Accounts>
    {
        [NotNull]
        public decimal AvailableBalance { get; }
        [NotNull]
        public decimal UsableBalance { get; }
        [NotNull]
        public decimal BankBalance { get; }
        [NotNull]
        public string Iban { get; }
        [NotNull]
        public string Bic { get; }
        [NotNull]
        public string BankName { get; }
        [NotNull]
        public bool Seized { get; }
        [NotNull]
        public Guid Id { get; }

        [JsonConstructor]
        internal Accounts(
            decimal? availableBalance,
            decimal? usableBalance,
            decimal? bankBalance,
            string iban,
            string bic,
            string bankName,
            bool? seized,
            Guid? id)
        {
            Guard.IsNotNull(availableBalance, nameof(availableBalance));
            Guard.IsNotNull(usableBalance, nameof(usableBalance));
            Guard.IsNotNull(bankBalance, nameof(bankBalance));
            Guard.IsNotNullOrEmpty(iban, nameof(iban));
            Guard.IsNotNullOrEmpty(bic, nameof(bic));
            Guard.IsNotNullOrEmpty(bankName, nameof(bankName));
            Guard.IsNotNull(seized, nameof(seized));
            Guard.IsNotNull(id, nameof(id));
            AvailableBalance = availableBalance.Value;
            UsableBalance = usableBalance.Value;
            BankBalance = bankBalance.Value;
            Iban = iban;
            Bic = bic;
            BankName = bankName;
            Seized = seized.Value;
            Id = id.Value;
        }

        [NotNull]
        public override string ToString() => $"{BankName}, {Iban}";

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Accounts a, Accounts b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Accounts a, Accounts b) => !(a == b);

        public static bool Equals(Accounts a, Accounts b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Accounts);

        public bool Equals(Accounts other) => this == other;
    }
}
