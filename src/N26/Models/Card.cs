using System;
using JetBrains.Annotations;
using N26.Json;
using N26.Utilities;
using Newtonsoft.Json;

namespace N26.Models
{
    #region Enums

    public enum CardType { Maestro, MasterCard }

    public enum CardStatus
    {
        Open = InternalCardStatus.OPEN,
        Blocked = InternalCardStatus.BLOCKED,
        Declared = InternalCardStatus.DECLARED,
        NotDeclared = InternalCardStatus.NOT_DECLARED,
        NotActive = InternalCardStatus.NOT_ACTIVE,
        Stolen = InternalCardStatus.STOLEN,
        Active = InternalCardStatus.M_ACTIVE,
        Linked = InternalCardStatus.M_LINKED,
        PhysicalUnconfirmedDisabled = InternalCardStatus.M_PHYSICAL_UNCONFIRMED_DISABLED,
        Disabled = InternalCardStatus.M_DISABLED
    }

    internal enum InternalCardStatus
    {
        OPEN,
        BLOCKED,
        DECLARED,
        NOT_DECLARED,
        NOT_ACTIVE,
        STOLEN,
        M_ACTIVE,
        M_LINKED,
        M_PHYSICAL_UNCONFIRMED_DISABLED,
        M_DISABLED
    }

    public enum CardProduct
    {
        BlackCardMonthly = InternalCardProduct.BLACK_CARD_MONTHLY,
        MaestroCard = InternalCardProduct.MAESTRO_CARD
    }

    internal enum InternalCardProduct { BLACK_CARD_MONTHLY, MAESTRO_CARD }

    public enum CardProductType { Black, Business, Maestro, Standard }

    #endregion

    public class Card : N26Model<Card>
    {
        [CanBeNull]
        public object PublicToken { get; }
        [CanBeNull]
        public string Pan { get; }
        [JsonRequired, NotNull]
        public string MaskedPan { get; }
        [JsonRequired]
        public DateTime ExpirationDate { get; }
        [JsonRequired]
        public CardType CardType { get; }
        [JsonRequired, JsonConverter(typeof(StringEnumConverter<InternalCardStatus>))]
        public CardStatus Status { get; }
        [JsonConverter(typeof(StringEnumConverter<InternalCardProduct?>)), CanBeNull]
        public CardProduct? CardProduct { get; }
        [JsonRequired]
        public CardProductType CardProductType { get; }
        [JsonRequired]
        public DateTime PinDefined { get; }
        [JsonRequired]
        public DateTime CardActivated { get; }
        [JsonRequired, NotNull]
        public string UserNameOnCard { get; }
        [CanBeNull]
        public object ExceetExpressCardDelivery { get; }
        [CanBeNull]
        public object Membership { get; }
        [CanBeNull]
        public object ExceetActualDeliveryDate { get; }
        [CanBeNull]
        public object ExceetExpressCardDeliveryEmailSent { get; }
        [CanBeNull]
        public object ExceetCardStatus { get; }
        [CanBeNull]
        public object ExceetExpectedDeliveryDate { get; }
        [CanBeNull]
        public object ExceetExpressCardDeliveryTrackingId { get; }
        [CanBeNull]
        public object CardSettingsId { get; }
        [JsonRequired]
        public bool ApplePayEligible { get; }
        [JsonRequired]
        public bool MptsCard { get; }

        [JsonConstructor]
        internal Card(
            IN26Client n26Client,
            Guid id,
            object publicToken,
            string pan,
            string maskedPan,
            DateTime expirationDate,
            CardType cardType,
            CardStatus status,
            CardProduct? cardProduct,
            CardProductType cardProductType,
            DateTime pinDefined,
            DateTime cardActivated,
            string userNameOnCard,
            object exceetExpressCardDelivery,
            object membership,
            object exceetActualDeliveryDate,
            object exceetExpressCardDeliveryEmailSent,
            object exceetCardStatus,
            object exceetExpectedDeliveryDate,
            object exceetExpressCardDeliveryTrackingId,
            object cardSettingsId,
            bool applePayEligible,
            bool mptsCard)
            : base(n26Client, id)
        {
#if DEBUG
            if (publicToken != null) throw new NotImplementedException();
#endif
            Guard.IsNotNullOrEmpty(maskedPan, nameof(maskedPan));
            Guard.IsNotNullOrEmpty(userNameOnCard, nameof(userNameOnCard));
#if DEBUG
            if (exceetExpressCardDelivery != null) throw new NotImplementedException();
            if (membership != null) throw new NotImplementedException();
            if (exceetActualDeliveryDate != null) throw new NotImplementedException();
            if (exceetExpressCardDeliveryEmailSent != null) throw new NotImplementedException();
            if (exceetCardStatus != null) throw new NotImplementedException();
            if (exceetExpectedDeliveryDate != null) throw new NotImplementedException();
            if (exceetExpressCardDeliveryTrackingId != null) throw new NotImplementedException();
            if (cardSettingsId != null) throw new NotImplementedException();
#endif
            PublicToken = publicToken;
            Pan = pan;
            MaskedPan = maskedPan;
            ExpirationDate = expirationDate;
            CardType = cardType;
            Status = status;
            CardProduct = cardProduct;
            CardProductType = cardProductType;
            PinDefined = pinDefined;
            CardActivated = cardActivated;
            UserNameOnCard = userNameOnCard;
            ExceetExpressCardDelivery = exceetExpressCardDelivery;
            Membership = membership;
            ExceetActualDeliveryDate = exceetActualDeliveryDate;
            ExceetExpressCardDeliveryEmailSent = exceetExpressCardDeliveryEmailSent;
            ExceetCardStatus = exceetCardStatus;
            ExceetExpectedDeliveryDate = exceetExpectedDeliveryDate;
            ExceetExpressCardDeliveryTrackingId = exceetExpressCardDeliveryTrackingId;
            CardSettingsId = cardSettingsId;
            ApplePayEligible = applePayEligible;
            MptsCard = mptsCard;
        }

        [NotNull]
        public override string ToString() => $"{CardType} {CardProductType}, {MaskedPan}, {ExpirationDate:y}";
    }
}
