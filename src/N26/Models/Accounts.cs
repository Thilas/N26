namespace N26.Models
{
    public class Accounts
    {
        public decimal AvailableBalance { get; set; }
        public decimal UsableBalance { get; set; }
        public decimal BankBalance { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string BankName { get; set; }
        public bool Seized { get; set; }
        public string Id { get; set; }
    }
}
