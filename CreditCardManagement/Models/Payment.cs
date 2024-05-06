namespace CreditCardManagement.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int CreditCardId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }


}
