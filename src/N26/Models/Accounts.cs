using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    [N26Model("api/accounts")]
    public sealed class Accounts : N26Model<Accounts>
    {
        [JsonRequired]
        public decimal AvailableBalance { get; }
        [JsonRequired]
        public decimal UsableBalance { get; }
        [JsonRequired]
        public decimal BankBalance { get; }
        [JsonRequired, NotNull]
        public string Iban { get; }
        [JsonRequired, NotNull]
        public string Bic { get; }
        [JsonRequired, NotNull]
        public string BankName { get; }
        [JsonRequired]
        public bool Seized { get; }

        [JsonConstructor]
        internal Accounts(
            [NotNull] IClient client,
            decimal availableBalance,
            decimal usableBalance,
            decimal bankBalance,
            [NotNull] string iban,
            [NotNull] string bic,
            [NotNull] string bankName,
            bool seized,
            Guid id)
            : base(client, id)
        {
            Guard.IsNotNullOrEmpty(iban, nameof(iban));
            Guard.IsNotNullOrEmpty(bic, nameof(bic));
            Guard.IsNotNullOrEmpty(bankName, nameof(bankName));
            AvailableBalance = availableBalance;
            UsableBalance = usableBalance;
            BankBalance = bankBalance;
            Iban = iban;
            Bic = bic;
            BankName = bankName;
            Seized = seized;
        }

        [NotNull]
        public override string ToString() => $"{BankName}, {Iban}";
    }
}
