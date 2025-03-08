using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Account;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class ChangeAccStatusDto
    {
        [EnumDataType(typeof(AccountStatus), ErrorMessage = "only active - inactive - closed")]
        public string accountStatus { get; set; }
    }
}
