using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Card;
using Banking_system.Enums.Loan;

namespace Banking_system.DTO_s.CardDto_s
{
    public class CardUpdateDto
    {
        // public string cardNumber { get; set; } = string.Empty;
        //public string cardType { get; set; }

      
        [CheckEnumValue(typeof(CardStatus), ErrorMessage = "only active - blocked - expired")]
        public string cardStatus { get; set; }
        //public int CustomerId { get; set; }
        public decimal moneyAvailable { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
