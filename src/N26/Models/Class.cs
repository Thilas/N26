using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{

    //#region transactions

    //public class Transactions
    //{
    //    public TransactionsPaging paging { get; set; }
    //    public List<Transaction> data { get; set; }
    //}

    //public class TransactionsPaging
    //{
    //    public string next { get; set; }
    //    public int totalResults { get; set; }
    //}

    //public class Transaction
    //{
    //    public string transactionId { get; set; }
    //    public string n26Iban { get; set; }
    //    public string bankTransferTypeText { get; set; }
    //    public string referenceText { get; set; }
    //    public string partnerBankName { get; set; }
    //    public string partnerName { get; set; }
    //    public string partnerIban { get; set; }
    //    public string partnerBic { get; set; }
    //    public bool partnerAccountIsSepa { get; set; }
    //    public string partnerBcn { get; set; }
    //    public float amount { get; set; }
    //    public Currencycode currencyCode { get; set; }
    //    public string linkId { get; set; }
    //    public bool recurring { get; set; }
    //    public string type { get; set; }
    //    public long visibleTS { get; set; }
    //    public string description { get; set; }
    //    public long confirmed { get; set; }
    //    public string userSmartCategory { get; set; }
    //    public string id { get; set; }
    //    public int mcc { get; set; }
    //    public int mccGroup { get; set; }
    //    public string uniqueTransactionId { get; set; }
    //    public string merchantName { get; set; }
    //    public string merchantCity { get; set; }
    //    public string merchantCountry { get; set; }
    //    public string merchantId { get; set; }
    //    public string terminalId { get; set; }
    //    public string transTerminal { get; set; }
    //    public float exchangeRate { get; set; }
    //    public float originalAmount { get; set; }
    //    public Originalcurrency originalCurrency { get; set; }
    //    public string cardType { get; set; }
    //    public string partnerAccountBan { get; set; }
    //    public long userCertified { get; set; }
    //    public int responseCode { get; set; }
    //    public float oldAmount { get; set; }
    //    public float newAmount { get; set; }
    //}

    //public class Currencycode
    //{
    //    public string currencyCode { get; set; }
    //}

    //public class Originalcurrency
    //{
    //    public string currencyCode { get; set; }
    //}

    //#endregion

    //#region addresses


    //public class Addresses
    //{
    //    public AddressesPaging paging { get; set; }
    //    public List<Address> data { get; set; }
    //}

    //public class AddressesPaging
    //{
    //    public int totalResults { get; set; }
    //}

    //public class Address
    //{
    //    public string addressLine1 { get; set; }
    //    public string streetName { get; set; }
    //    public string houseNumberBlock { get; set; }
    //    public string zipCode { get; set; }
    //    public string cityName { get; set; }
    //    public string countryName { get; set; }
    //    public string type { get; set; }
    //    public string id { get; set; }
    //}


    //#endregion

    //#region cards


    //public class Cards
    //{
    //    public CardsPaging paging { get; set; }
    //    public List<Card> data { get; set; }
    //}

    //public class CardsPaging
    //{
    //    public int totalResults { get; set; }
    //}

    //public class Card
    //{
    //    public string maskedPan { get; set; }
    //    public long expirationDate { get; set; }
    //    public string cardType { get; set; }
    //    public bool exceetExpressCardDelivery { get; set; }
    //    public bool exceetExpressCardDeliveryEmailSent { get; set; }
    //    public string n26Status { get; set; }
    //    public long pinDefined { get; set; }
    //    public long cardActivated { get; set; }
    //    public string usernameOnCard { get; set; }
    //    public string id { get; set; }
    //}

    //#endregion

    //#region accounts


    //public class Accounts
    //{
    //    public string status { get; set; }
    //    public float availableBalance { get; set; }
    //    public float usableBalance { get; set; }
    //    public float bankBalance { get; set; }
    //    public string iban { get; set; }
    //    public string id { get; set; }
    //}


    //#endregion

    //#region me


    //public class Me
    //{
    //    public string email { get; set; }
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public string kycFirstName { get; set; }
    //    public string kycLastName { get; set; }
    //    public string title { get; set; }
    //    public string gender { get; set; }
    //    public long birthDate { get; set; }
    //    public string passwordHash { get; set; }
    //    public bool signupCompleted { get; set; }
    //    public string nationality { get; set; }
    //    public string birthPlace { get; set; }
    //    public string mobilePhoneNumber { get; set; }
    //    public bool transferWiseTermsAccepted { get; set; }
    //    public string shadowID { get; set; }
    //    public string id { get; set; }
    //}


    //#endregion
}
