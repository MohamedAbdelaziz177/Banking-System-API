using Banking_system.DAL.Enums.Transactions;
using Banking_system.DTO_s.TransactionDto_s;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.CustomValidations
{
    public class TransactionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var trx = (TransactionCreateDto)validationContext.ObjectInstance;

            if (trx.TrxType == TransactionType.deposit.ToString() && trx.ToAccountId == null)
                return new ValidationResult("ToAccountId must not be null for deposit transactions.");

            if(trx.TrxType == TransactionType.withdrawal.ToString() && trx.FromAccountId == null)
                return new ValidationResult("FromAccountId must not be null for withdrawal transactions.");

            return ValidationResult.Success;

  
        }
    }
}
