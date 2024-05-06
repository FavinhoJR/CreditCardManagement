namespace CreditCardManagement.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CreditCardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

