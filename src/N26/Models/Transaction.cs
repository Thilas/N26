using System;
using JetBrains.Annotations;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    #region Enums

    public enum Currency { AED, AUD, BGN, CAD, CHF, CNY, CZK, DKK, EUR, GBP, GEL, HUF, INR, NOK, NZD, PLN, RON, SEK, UAH, USD }

    public enum TransactionNature { Normal }

    public enum TransactionType
    {
        MasterCardPaymentAA = InternalTransactionType.AA,
        MasterCardPaymentAE = InternalTransactionType.AE,
        MasterCardPaymentAV = InternalTransactionType.AV,
        IncomeCT = InternalTransactionType.CT,
        OutgoingTransferDT = InternalTransactionType.DT,
        MasterCardPaymentPT = InternalTransactionType.PT,
        OutgoingTransferWEE = InternalTransactionType.WEE
    }

    internal enum InternalTransactionType { AA, AE, AV, CT, DT, PT, WEE }

    #endregion

    public class Transaction
    {
        [NotNull]
        public Guid Id { get; }
        [NotNull]
        public Guid UserId { get; }
        [NotNull]
        public TransactionType Type { get; }
        [NotNull]
        public decimal Amount { get; }
        [NotNull]
        public Currency CurrencyCode { get; }
        [CanBeNull]
        public decimal? OriginalAmount { get; }
        [CanBeNull]
        public Currency? OriginalCurrency { get; }
        [CanBeNull]
        public decimal? ExchangeRate { get; }
        [CanBeNull]
        public string MerchantCity { get; }
        [NotNull]
        public DateTime VisibleTS { get; }
        [CanBeNull]
        public int? Mcc { get; }
        [CanBeNull]
        public int? MccGroup { get; }
        [CanBeNull]
        public string MerchantName { get; }
        [NotNull]
        public bool Recurring { get; }
        [NotNull]
        public Guid AccountId { get; }
        [NotNull]
        public string Category { get; }
        [CanBeNull]
        public Guid? CardId { get; }
        [NotNull]
        public DateTime UserCertified { get; }
        [NotNull]
        public bool Pending { get; }
        [NotNull]
        public TransactionNature TransactionNature { get; }
        [NotNull]
        public DateTime CreatedTS { get; }
        [CanBeNull]
        public int? MerchantCountry { get; }
        [NotNull]
        public Guid SmartLinkId { get; }
        [NotNull]
        public Guid LinkId { get; }
        [NotNull]
        public DateTime Confirmed { get; }

        [JsonConstructor]
        internal Transaction(
            Guid? id, Guid? userId, InternalTransactionType? type,
            decimal? amount, Currency? currencyCode, decimal? originalAmount, Currency? originalCurrency, decimal? exchangeRate,
            string merchantCity, long? visibleTS, int? mcc, int? mccGroup, string merchantName, bool? recurring,
            Guid? accountId, string category, Guid? cardId, long? userCertified, bool? pending, TransactionNature? transactionNature,
            long? createdTS, int? merchantCountry, Guid? smartLinkId, Guid? linkId, long? confirmed)
            : this(
                  id, userId, EnumHelper.Convert<InternalTransactionType, TransactionType>(type),
                  amount, currencyCode, originalAmount, originalCurrency, exchangeRate,
                  merchantCity, DateTimeHelper.FromJsDate(visibleTS), mcc, mccGroup, merchantName, recurring,
                  accountId, category, cardId, DateTimeHelper.FromJsDate(userCertified), pending, transactionNature,
                  DateTimeHelper.FromJsDate(createdTS), merchantCountry, smartLinkId, linkId, DateTimeHelper.FromJsDate(confirmed))
        {
        }

        private Transaction(
            Guid? id,
            Guid? userId,
            TransactionType? type,
            decimal? amount,
            Currency? currencyCode,
            decimal? originalAmount,
            Currency? originalCurrency,
            decimal? exchangeRate,
            string merchantCity,
            DateTime? visibleTS,
            int? mcc,
            int? mccGroup,
            string merchantName,
            bool? recurring,
            Guid? accountId,
            string category,
            Guid? cardId,
            DateTime? userCertified,
            bool? pending,
            TransactionNature? transactionNature,
            DateTime? createdTS,
            int? merchantCountry,
            Guid? smartLinkId,
            Guid? linkId,
            DateTime? confirmed)
        {
            Guard.IsNotNull(id, nameof(id));
            Guard.IsNotNull(userId, nameof(userId));
            Guard.IsNotNull(type, nameof(type));
            Guard.IsNotNull(amount, nameof(amount));
            Guard.IsNotNull(currencyCode, nameof(currencyCode));
            Guard.IsNotNull(visibleTS, nameof(visibleTS));
            Guard.IsNotNull(recurring, nameof(recurring));
            Guard.IsNotNull(accountId, nameof(accountId));
            Guard.IsNotNullNorEmpty(category, nameof(category));
            Guard.IsNotNull(userCertified, nameof(userCertified));
            Guard.IsNotNull(pending, nameof(pending));
            Guard.IsNotNull(transactionNature, nameof(transactionNature));
            Guard.IsNotNull(createdTS, nameof(createdTS));
            Guard.IsNotNull(smartLinkId, nameof(smartLinkId));
            Guard.IsNotNull(linkId, nameof(linkId));
            Guard.IsNotNull(confirmed, nameof(confirmed));
            Id = id.Value;
            UserId = userId.Value;
            Type = type.Value;
            Amount = amount.Value;
            CurrencyCode = currencyCode.Value;
            OriginalAmount = originalAmount;
            OriginalCurrency = originalCurrency;
            ExchangeRate = exchangeRate;
            MerchantCity = merchantCity;
            VisibleTS = visibleTS.Value;
            Mcc = mcc;
            MccGroup = mccGroup;
            MerchantName = merchantName;
            Recurring = recurring.Value;
            AccountId = accountId.Value;
            Category = category;
            CardId = cardId;
            UserCertified = userCertified.Value;
            Pending = pending.Value;
            TransactionNature = transactionNature.Value;
            CreatedTS = createdTS.Value;
            MerchantCountry = merchantCountry;
            SmartLinkId = smartLinkId.Value;
            LinkId = linkId.Value;
            Confirmed = confirmed.Value;
        }

        [NotNull]
        public override string ToString() => $"{Type}{(string.IsNullOrEmpty(MerchantName) ? null : $", {MerchantName}")}, {VisibleTS:d}, {Amount} {CurrencyCode}";

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Transaction a, Transaction b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Transaction a, Transaction b) => !(a == b);

        public static bool Equals(Transaction a, Transaction b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Transaction);

        public bool Equals(Transaction other) => this == other;
    }
}
