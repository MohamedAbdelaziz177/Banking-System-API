using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Account;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class ChangeAccTypeDto
    {
        [CheckEnumValue(typeof(AccountType), ErrorMessage = "only savings - checking - business")]
        public string accountType { get; set; }
    }
}
