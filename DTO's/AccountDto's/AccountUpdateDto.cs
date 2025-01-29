using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Account;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class AccountUpdateDto
    {
        
        [CheckEnumValue(typeof(AccountStatus), ErrorMessage = "only active - inactive - closed")]
        public string AccountStatus { get; set; }

        [CheckEnumValue(typeof(AccountType), ErrorMessage = "only savings - checking - business")]
        public string AccountType { get; set; }
        public decimal balance { get; set; }
    }
}
