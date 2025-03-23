using Banking_system.Enums.Loan;

namespace Banking_system.DTO_s.LoanDto_s
{
    public class LoanReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

     //   public string CustomerName { get; set; } = string.Empty;

        public decimal amount { get; set; }

        public string loanStatus { get; set; }

        public decimal InterestRate { get; set; }
    }
}
