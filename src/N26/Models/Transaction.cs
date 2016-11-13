using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Transaction
    {
        public string transactionId { get; set; }
        public string n26Iban { get; set; }
        public string bankTransferTypeText { get; set; }
        public string referenceText { get; set; }
        public string partnerBankName { get; set; }
        public string partnerName { get; set; }
        public string partnerIban { get; set; }
        public string partnerBic { get; set; }
        public bool partnerAccountIsSepa { get; set; }
        public string partnerBcn { get; set; }
        public float amount { get; set; }
        public Currencycode currencyCode { get; set; }
        public string linkId { get; set; }
        public bool recurring { get; set; }
        public string type { get; set; }
        public long visibleTS { get; set; }
        public string description { get; set; }
        public long confirmed { get; set; }
        public string userSmartCategory { get; set; }
        public string id { get; set; }
        public int mcc { get; set; }
        public int mccGroup { get; set; }
        public string uniqueTransactionId { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public string merchantCountry { get; set; }
        public string merchantId { get; set; }
        public string terminalId { get; set; }
        public string transTerminal { get; set; }
        public float exchangeRate { get; set; }
        public float originalAmount { get; set; }
        public Originalcurrency originalCurrency { get; set; }
        public string cardType { get; set; }
        public string partnerAccountBan { get; set; }
        public long userCertified { get; set; }
        public int responseCode { get; set; }
        public float oldAmount { get; set; }
        public float newAmount { get; set; }
    }
}
