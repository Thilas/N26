using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
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
        public string AccountId { get; set; }
        public string Category { get; set; }
        public string CardId { get; set; }
        [JsonProperty("userCertified")]
        private long _userCertified;
        public DateTime UserCertified
        {
            get { return DateTimeHelper.FromJsDate(_userCertified); }
            set { _userCertified = DateTimeHelper.ToJsDate(value); }
        }
        public bool Pending { get; set; }
        public string TransactionNature { get; set; }
        [JsonProperty("createdTS")]
        private long _createdTS;
        public DateTime CreatedTS
        {
            get { return DateTimeHelper.FromJsDate(_createdTS); }
            set { _createdTS = DateTimeHelper.ToJsDate(value); }
        }
        public int MerchantCountry { get; set; }
        public string SmartLinkId { get; set; }
        public string LinkId { get; set; }
        [JsonProperty("confirmed")]
        private long _confirmed;
        public DateTime Confirmed
        {
            get { return DateTimeHelper.FromJsDate(_confirmed); }
            set { _confirmed = DateTimeHelper.ToJsDate(value); }
        }
    }
}
