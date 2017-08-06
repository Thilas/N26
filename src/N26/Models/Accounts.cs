﻿using System;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    public class Accounts : N26Model<Accounts>
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
            IN26Client n26Client,
            decimal availableBalance,
            decimal usableBalance,
            decimal bankBalance,
            string iban,
            string bic,
            string bankName,
            bool seized,
            Guid id)
            : base(n26Client, id)
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
