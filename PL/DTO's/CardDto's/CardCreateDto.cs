using Banking_system.DAL.Enums.Card;
using Banking_system.Enums.Loan;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.CardDto_s
{
    public class CardCreateDto
    {
        // public string cardNumber { get; set; } = string.Empty;

        [EnumDataType(typeof(CardType), ErrorMessage = "only debit - credit - prepaid")]
        public string cardType { get; set; }
       // public string cardStatus { get; set; }
        public int CustomerId { get; set; }
        public decimal amount { get; set; } = 0;
       // public bool isExpired { get; set; }
    }
}
