using Banking_system.Enums.Loan;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.LoanDto_s
{
    public class ChangeLoanStatusDto
    {
        [EnumDataType(typeof(LoanStatus), ErrorMessage = "only active - paid - defaulted")]
        public string loanStatus { get; set; }
    }
}
