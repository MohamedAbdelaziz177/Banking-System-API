using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Loan;
using Banking_system.Enums.Transactions;

namespace Banking_system.DTO_s.TransactionDto_s
{
    public class TransactionCreateDto
    {
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }

        [CheckEnumValue(typeof(TransactionType), ErrorMessage = "only deposit - withdrawal - transfer - payment")]
        public string TrxType { get; set; }
        public decimal amount { get; set; }
    }
}
