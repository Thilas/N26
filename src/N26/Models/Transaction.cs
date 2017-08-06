using System;
using JetBrains.Annotations;
using N26.Json;
using N26.Utilities;
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

    public class Transaction : N26Model<Transaction>
    {
        [JsonRequired]
        public Guid UserId { get; }
        [JsonRequired, JsonConverter(typeof(StringEnumConverter<InternalTransactionType>))]
        public TransactionType Type { get; }
        [JsonRequired]
        public decimal Amount { get; }
        [JsonRequired]
        public Currency CurrencyCode { get; }
        [CanBeNull]
        public decimal? OriginalAmount { get; }
        [CanBeNull]
        public Currency? OriginalCurrency { get; }
        [CanBeNull]
        public decimal? ExchangeRate { get; }
        [CanBeNull]
        public string MerchantCity { get; }
        [JsonRequired]
        public DateTime VisibleTS { get; }
        [CanBeNull]
        public int? Mcc { get; }
        [CanBeNull]
        public int? MccGroup { get; }
        [CanBeNull]
        public string MerchantName { get; }
        [JsonRequired]
        public bool Recurring { get; }
        [CanBeNull]
        public string PartnerBic { get; }
        [CanBeNull]
        public string PartnerBcn { get; }
        [CanBeNull]
        public bool? PartnerAccountIsSepa { get; }
        [CanBeNull]
        public string PartnerBankName { get; }
        [CanBeNull]
        public string PartnerName { get; }
        [JsonRequired]
        public Guid AccountId { get; }
        [CanBeNull]
        public string PartnerIban { get; }
        [CanBeNull]
        public string PartnerAccountBan { get; }
        [JsonRequired, NotNull]
        public string Category { get; }
        [CanBeNull]
        public Guid? CardId { get; }
        [CanBeNull]
        public string ReferenceText { get; }
        [CanBeNull]
        public DateTime? UserAccepted { get; }
        [JsonRequired]
        public DateTime UserCertified { get; }
        [JsonRequired]
        public bool Pending { get; }
        [JsonRequired]
        public TransactionNature TransactionNature { get; }
        [CanBeNull]
        public string ReferenceToOriginalOperation { get; }
        [CanBeNull]
        public Guid? SmartContactId { get; }
        [JsonRequired]
        public DateTime CreatedTS { get; }
        [CanBeNull]
        public int? MerchantCountry { get; }
        [JsonRequired]
        public Guid SmartLinkId { get; }
        [JsonRequired]
        public Guid LinkId { get; }
        [JsonRequired]
        public DateTime Confirmed { get; }

        [JsonConstructor]
        internal Transaction(
            IN26Client n26Client,
            Guid id,
            Guid userId,
            TransactionType type,
            decimal amount,
            Currency currencyCode,
            decimal? originalAmount,
            Currency? originalCurrency,
            decimal? exchangeRate,
            string merchantCity,
            DateTime visibleTS,
            int? mcc,
            int? mccGroup,
            string merchantName,
            bool recurring,
            string partnerBic,
            string partnerBcn,
            bool? partnerAccountIsSepa,
            string partnerBankName,
            string partnerName,
            Guid accountId,
            string partnerIban,
            string partnerAccountBan,
            string category,
            Guid? cardId,
            string referenceText,
            DateTime? userAccepted,
            DateTime userCertified,
            bool pending,
            TransactionNature transactionNature,
            string referenceToOriginalOperation,
            Guid? smartContactId,
            DateTime createdTS,
            int? merchantCountry,
            Guid smartLinkId,
            Guid linkId,
            DateTime confirmed)
            : base(n26Client, id)
        {
            Guard.IsNotNullOrEmpty(category, nameof(category));
            UserId = userId;
            Type = type;
            Amount = amount;
            CurrencyCode = currencyCode;
            OriginalAmount = originalAmount;
            OriginalCurrency = originalCurrency;
            ExchangeRate = exchangeRate;
            MerchantCity = merchantCity;
            VisibleTS = visibleTS;
            Mcc = mcc;
            MccGroup = mccGroup;
            MerchantName = merchantName;
            Recurring = recurring;
            PartnerBic = partnerBic;
            PartnerBcn = partnerBcn;
            PartnerAccountIsSepa = partnerAccountIsSepa;
            PartnerBankName = partnerBankName;
            PartnerName = partnerName;
            AccountId = accountId;
            PartnerIban = partnerIban;
            PartnerAccountBan = partnerAccountBan;
            Category = category;
            CardId = cardId;
            ReferenceText = referenceText;
            UserAccepted = userAccepted;
            UserCertified = userCertified;
            Pending = pending;
            TransactionNature = transactionNature;
            ReferenceToOriginalOperation = referenceToOriginalOperation;
            SmartContactId = smartContactId;
            CreatedTS = createdTS;
            MerchantCountry = merchantCountry;
            SmartLinkId = smartLinkId;
            LinkId = linkId;
            Confirmed = confirmed;
        }

        [NotNull]
        public override string ToString() => $"{MerchantName ?? PartnerName}, {VisibleTS:d}, {Amount} {CurrencyCode}";
    }
}
