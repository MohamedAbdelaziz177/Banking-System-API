using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Account;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class ChangeAccStatusDto
    {
        [CheckEnumValue(typeof(AccountStatus), ErrorMessage = "only active - inactive - closed")]
        public string accountStatus { get; set; }
    }
}
