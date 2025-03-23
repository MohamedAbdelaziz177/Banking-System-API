using Banking_system.DAL.Enums.Loan;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.LoanDto_s
{
    public class LoanUpdateDto
    {
        public decimal amount { get; set; }

        [EnumDataType(typeof(LoanStatus), ErrorMessage = "only active - paid - defaulted")]
        public string loanStatus { get; set; }

        [Range(10, 50)]
        public decimal InterestRate { get; set; }
    }
}
