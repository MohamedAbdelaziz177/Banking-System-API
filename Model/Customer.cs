using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_system.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [ForeignKey(nameof(appUser))]
        public int UserId { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Loan> loans { get; set; } = new List<Loan>();
        public ICollection<Card> cards { get; set; } = new List<Card>();

        public AppUser appUser {  get; set; } 
    }
}
