using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum TransactionNature { Normal }

    public enum TransactionType
    {
        //[EnumMember(Value = "AA")]
        AA,
        //[EnumMember(Value = "AV")]
        AV,
        //[EnumMember(Value = "DT")]
        DT,
        //[EnumMember(Value = "PT")]
        PT
    }

    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public decimal OriginalAmount { get; set; }

        public string OriginalCurrency { get; set; }

        public decimal ExchangeRate { get; set; }

        public string MerchantCity { get; set; }

        [JsonProperty("visibleTS")]
        private long _visibleTS;
        public DateTime VisibleTS
        {
            get { return DateTimeHelper.FromJsDate(_visibleTS); }
            set { _visibleTS = DateTimeHelper.ToJsDate(value); }
        }

        public int Mcc { get; set; }

        public int MccGroup { get; set; }

        public string MerchantName { get; set; }

        public bool Recurring { get; set; }

        public Guid AccountId { get; set; }

        public string Category { get; set; }

        public Guid CardId { get; set; }

        [JsonProperty("userCertified")]
        private long _userCertified;
        public DateTime UserCertified
        {
            get { return DateTimeHelper.FromJsDate(_userCertified); }
            set { _userCertified = DateTimeHelper.ToJsDate(value); }
        }

        public bool Pending { get; set; }

        public TransactionNature TransactionNature { get; set; }

        [JsonProperty("createdTS")]
        private long _createdTS;
        public DateTime CreatedTS
        {
            get { return DateTimeHelper.FromJsDate(_createdTS); }
            set { _createdTS = DateTimeHelper.ToJsDate(value); }
        }

        public int MerchantCountry { get; set; }

        public Guid SmartLinkId { get; set; }

        public Guid LinkId { get; set; }

        [JsonProperty("confirmed")]
        private long _confirmed;
        public DateTime Confirmed
        {
            get { return DateTimeHelper.FromJsDate(_confirmed); }
            set { _confirmed = DateTimeHelper.ToJsDate(value); }
        }
    }
}
