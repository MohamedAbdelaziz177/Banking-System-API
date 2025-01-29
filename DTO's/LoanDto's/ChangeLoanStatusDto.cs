using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Loan;

namespace Banking_system.DTO_s.LoanDto_s
{
    public class ChangeLoanStatusDto
    {
        [CheckEnumValue(typeof(LoanStatus), ErrorMessage = "only active - paid - defaulted")]
        public string loanStatus { get; set; }
    }
}
