using System;
using N26.Helpers;
using Newtonsoft.Json;

namespace N26.Models
{
    public enum CardType { Maestro, MasterCard }

    public enum CardStatus
    {
        Open,
        Blocked,
        Declared,
        //[EnumMember(Value = "NOT_DECLARED")]
        //NotDeclared,
        Not_Declared,
        //[EnumMember(Value = "NOT_ACTIVE")]
        //NotActive,
        Not_Active,
        Stolen,
        //[EnumMember(Value = "M_ACTIVE")]
        //Active,
        M_Active,
        //[EnumMember(Value = "M_LINKED")]
        //Linked,
        M_Linked,
        //[EnumMember(Value = "M_PHYSICAL_UNCONFIRMED_DISABLED")]
        //PhysicalUnconfirmedDisabled,
        M_Physical_Unconfirmed_Disabled,
        //[EnumMember(Value = "M_DISABLED")]
        //Disabled
        M_Disabled
    }

    public enum CardProduct
    {
        //[EnumMember(Value = "BLACK_CARD_MONTHLY")]
        //BlackCardMonthly,
        Black_Card_Monthly,
        //[EnumMember(Value = "MAESTRO_CARD")]
        //MaestroCard
        Maestro_Card
    }

    public enum CardProductType { Black, Business, Maestro, Standard }

    public class Card
    {
        public Guid Id { get; set; }

        //public string PublicToken { get; set; }

        public string Pan { get; set; }

        public string MaskedPan { get; set; }

        [JsonProperty("expirationDate")]
        private long _expirationDate;
        public DateTime ExpirationDate
        {
            get { return DateTimeHelper.FromJsDate(_expirationDate); }
            set { _expirationDate = DateTimeHelper.ToJsDate(value); }
        }

        public CardType CardType { get; set; }

        public CardStatus Status { get; set; }

        public CardProduct? CardProduct { get; set; }

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

        public string UserNameOnCard { get; set; }

        //public string ExceetExpressCardDelivery { get; set; }

        //public string Membership { get; set; }

        //public string ExceetActualDeliveryDate { get; set; }

        //public string ExceetExpressCardDeliveryEmailSent { get; set; }

        //public string ExceetCardStatus { get; set; }

        //public string ExceetExpectedDeliveryDate { get; set; }

        //public string ExceetExpressCardDeliveryTrackingId { get; set; }

        //public string CardSettingsId { get; set; }

        public bool ApplePayEligible { get; set; }

        public bool MptsCard { get; set; }
    }
}
