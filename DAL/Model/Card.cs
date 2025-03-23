using Banking_system.Enums.Card;

namespace Banking_system.DAL.Model
{
    public class Card
    {
        public int Id { get; set; }

        public string cardNumber { get; set; }

        public int customerId { get; set; }

        public CardType cardType { get; set; }

        public CardStatus cardStatus { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddYears(2);

        public decimal amount { get; set; }
        public decimal? Limit { get; set; } // if the card is debit / prepaid



        //  public Customer customer { get; set; }




    }
}
