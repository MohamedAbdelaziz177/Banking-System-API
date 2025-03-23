using Banking_system.DTO_s.AccountDto_s;
using Banking_system.DTO_s.CardDto_s;
using Banking_system.DTO_s.LoanDto_s;

namespace Banking_system.DTO_s.CustomerDto_s
{
    public class CustomerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string address { get; set; }

        public List<AccountReadDto> custAccounts { get; set; } = new List<AccountReadDto>();
        public List<LoanReadDto> CustLoans {  get; set; } = new List<LoanReadDto>();
        public List<CardReadDto> CardLoans { get; set; } = new List<CardReadDto>();

    }
}
