using System;
using JetBrains.Annotations;
using N26.Helpers;
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

    public class Card : IEquatable<Card>
    {
        [NotNull]
        public Guid Id { get; }
        //public ??? PublicToken { get; }
        [CanBeNull]
        public string Pan { get; }
        [NotNull]
        public string MaskedPan { get; }
        [NotNull]
        public DateTime ExpirationDate { get; }
        [NotNull]
        public CardType CardType { get; }
        [NotNull]
        public CardStatus Status { get; }
        [CanBeNull]
        public CardProduct? CardProduct { get; }
        [NotNull]
        public CardProductType CardProductType { get; }
        [NotNull]
        public DateTime PinDefined { get; }
        [NotNull]
        public DateTime CardActivated { get; }
        [NotNull]
        public string UserNameOnCard { get; }
        //public ??? ExceetExpressCardDelivery { get; }
        //public ??? Membership { get; }
        //public ??? ExceetActualDeliveryDate { get; }
        //public ??? ExceetExpressCardDeliveryEmailSent { get; }
        //public ??? ExceetCardStatus { get; }
        //public ??? ExceetExpectedDeliveryDate { get; }
        //public ??? ExceetExpressCardDeliveryTrackingId { get; }
        //public ??? CardSettingsId { get; }
        [NotNull]
        public bool ApplePayEligible { get; }
        [NotNull]
        public bool MptsCard { get; }

        [JsonConstructor]
        internal Card(
            Guid? id, object publicToken, string pan, string maskedPan, long? expirationDate, CardType? cardType,
            InternalCardStatus? status, InternalCardProduct? cardProduct,
            CardProductType? cardProductType, long? pinDefined, long? cardActivated, string userNameOnCard,
            object exceetExpressCardDelivery, object membership, object exceetActualDeliveryDate, object exceetExpressCardDeliveryEmailSent,
            object exceetCardStatus, object exceetExpectedDeliveryDate, object exceetExpressCardDeliveryTrackingId, object cardSettingsId,
            bool? applePayEligible, bool? mptsCard)
            : this(
                  id, publicToken, pan, maskedPan, DateTimeHelper.FromJsDate(expirationDate), cardType,
                  EnumHelper.Convert<InternalCardStatus, CardStatus>(status), EnumHelper.Convert<InternalCardProduct, CardProduct>(cardProduct),
                  cardProductType, DateTimeHelper.FromJsDate(pinDefined), DateTimeHelper.FromJsDate(cardActivated), userNameOnCard,
                  exceetExpressCardDelivery, membership, exceetActualDeliveryDate, exceetExpressCardDeliveryEmailSent,
                  exceetCardStatus, exceetExpectedDeliveryDate, exceetExpressCardDeliveryTrackingId, cardSettingsId,
                  applePayEligible, mptsCard)
        {
        }

        private Card(
            Guid? id,
            object publicToken,
            string pan,
            string maskedPan,
            DateTime? expirationDate,
            CardType? cardType,
            CardStatus? status,
            CardProduct? cardProduct,
            CardProductType? cardProductType,
            DateTime? pinDefined,
            DateTime? cardActivated,
            string userNameOnCard,
            object exceetExpressCardDelivery,
            object membership,
            object exceetActualDeliveryDate,
            object exceetExpressCardDeliveryEmailSent,
            object exceetCardStatus,
            object exceetExpectedDeliveryDate,
            object exceetExpressCardDeliveryTrackingId,
            object cardSettingsId,
            bool? applePayEligible,
            bool? mptsCard)
        {
            Guard.IsNotNull(id, nameof(id));
#if DEBUG
            if (publicToken != null) throw new NotImplementedException();
#endif
            Guard.IsNotNullNorEmpty(maskedPan, nameof(maskedPan));
            Guard.IsNotNull(expirationDate, nameof(expirationDate));
            Guard.IsNotNull(cardType, nameof(cardType));
            Guard.IsNotNull(status, nameof(status));
            Guard.IsNotNull(cardProductType, nameof(cardProductType));
            Guard.IsNotNull(pinDefined, nameof(pinDefined));
            Guard.IsNotNull(cardActivated, nameof(cardActivated));
            Guard.IsNotNullNorEmpty(userNameOnCard, nameof(userNameOnCard));
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
            Guard.IsNotNull(applePayEligible, nameof(applePayEligible));
            Guard.IsNotNull(mptsCard, nameof(mptsCard));
            Id = id.Value;
            //PublicToken = publicToken;
            Pan = pan;
            MaskedPan = maskedPan;
            ExpirationDate = expirationDate.Value;
            CardType = cardType.Value;
            Status = status.Value;
            CardProduct = cardProduct;
            CardProductType = cardProductType.Value;
            PinDefined = pinDefined.Value;
            CardActivated = cardActivated.Value;
            UserNameOnCard = userNameOnCard;
            //ExceetExpressCardDelivery = exceetExpressCardDelivery;
            //Membership = membership;
            //ExceetActualDeliveryDate = exceetActualDeliveryDate;
            //ExceetExpressCardDeliveryEmailSent = exceetExpressCardDeliveryEmailSent;
            //ExceetCardStatus = exceetCardStatus;
            //ExceetExpectedDeliveryDate = exceetExpectedDeliveryDate;
            //ExceetExpressCardDeliveryTrackingId = exceetExpressCardDeliveryTrackingId;
            //CardSettingsId = cardSettingsId;
            ApplePayEligible = applePayEligible.Value;
            MptsCard = mptsCard.Value;
        }

        [NotNull]
        public override string ToString() => $"{CardType} {CardProductType}, {MaskedPan}, {ExpirationDate:y}";

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Card a, Card b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Card a, Card b) => !(a == b);

        public static bool Equals(Card a, Card b) => a == b;

        public override bool Equals(object obj) => Equals(obj as Card);

        public bool Equals(Card other) => this == other;
    }
}
