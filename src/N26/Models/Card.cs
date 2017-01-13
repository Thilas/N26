using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum CardType { Maestro, MasterCard }
    public enum Status { Open, Blocked, Declared, Not_Declared, Not_Active, Stolen, M_Active, M_Linked, M_Physical_Unconfirmed_Disabled, M_Disabled }
    //public enum ProductId { Black_Card_Monthly, Maestro_Card }
    public enum CardProductType { Black, Business, Maestro, Standard }

    public class Card
    {
        public string Id { get; set; }
        //public string PublicToken { get; set; }
        //public string Pan { get; set; }
        public string MaskedPan { get; set; }
        [JsonProperty("expirationDate")]
        private long _expirationDate;
        public DateTime ExpirationDate
        {
            get { return DateTimeHelper.FromJsDate(_expirationDate); }
            set { _expirationDate = DateTimeHelper.ToJsDate(value); }
        }
        public CardType CardType { get; set; }
        public Status Status { get; set; }
        //public ProductId CardProduct { get; set; }
        public CardProductType CardProductType { get; set; }
        [JsonProperty("pinDefined")]
        private long _pinDefined;
        public DateTime PinDefined
        {
            get { return DateTimeHelper.FromJsDate(_pinDefined); }
            set { _pinDefined = DateTimeHelper.ToJsDate(value); }
        }
        [JsonProperty("cardActivated")]
        private long _cardActivated;
        public DateTime CardActivated
        {
            get { return DateTimeHelper.FromJsDate(_cardActivated); }
            set { _cardActivated = DateTimeHelper.ToJsDate(value); }
        }
        public string UsernameOnCard { get; set; }
        //public string ExceetExpressCardDelivery { get; set; }
        //public string Membership { get; set; }
        //public string ExceetActualDeliveryDate { get; set; }
        //public string ExceetExpressCardDeliveryEmailSent { get; set; }
        //public string ExceetCardStatus { get; set; }
        //public string ExceetExpectedDeliveryDate { get; set; }
        //public string ExceetExpressCardDeliveryTrackingId { get; set; }
        public bool MptsCard { get; set; }
    }
}
