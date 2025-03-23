using Banking_system.DAL.Enums.Account;

namespace Banking_system.DAL.Model
{
    public class Account
    {
        public int Id { get; set; }

        public int customerId { get; set; }

        public decimal balance { get; set; }

        public AccountType accountType { get; set; }

        public AccountStatus accountStatus { get; set; }

        // public Customer customer { get; set; }

        //  public ICollection<Transaction> transactions { get; set; } = new List<Transaction>();


    }
}
