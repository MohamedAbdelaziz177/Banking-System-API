using Banking_system.DTO_s.TransactionDto_s;
using System.Transactions;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class AccountReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
       // public string CustomerName { get; set; } = string.Empty;
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public decimal Balance { get; set; }

        public List<TransactionReadDto> transactions { get; set; } = new List<TransactionReadDto>();
    }
}
