using Banking_system.DAL.Enums.Card;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.CardDto_s
{
    public class CardUpdateDto
    {
        // public string cardNumber { get; set; } = string.Empty;
        //public string cardType { get; set; }

      
        [EnumDataType(typeof(CardStatus), ErrorMessage = "only active - blocked - expired")]
        public string cardStatus { get; set; }
        //public int CustomerId { get; set; }
        public decimal amount { get; set; }
        public DateTime ExpiryDate { get; set; } 
    }
}
