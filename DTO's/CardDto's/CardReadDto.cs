using Banking_system.Enums.Card;

namespace Banking_system.DTO_s.CardDto_s
{
    public class CardReadDto
    {
        public int Id { get; set; }
        public string cardNumber { get; set; } = string.Empty;
        public string cardType { get; set; }
        public string cardStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal amount { get; set; }
        public DateTime ExpiryDate { get; set; } 

    }
}
