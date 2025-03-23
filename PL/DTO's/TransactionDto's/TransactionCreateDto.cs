using Banking_system.DAL.Enums.Transactions;
using Banking_system.DTO_s.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.TransactionDto_s
{
    public class TransactionCreateDto
    {
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }

        [EnumDataType(typeof(TransactionType), ErrorMessage = "only deposit - withdrawal - transfer - payment")]
        public string TrxType { get; set; }
        public decimal amount { get; set; }

        [TransactionValidation]
        public object TrxValidatorTrigger { get; set; } = new() { };
    }
}
