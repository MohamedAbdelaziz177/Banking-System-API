using Banking_system.DAL.Enums.Account;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class ChangeAccTypeDto
    {
        [EnumDataType(typeof(AccountType), ErrorMessage = "only savings - checking - business")]
        public string accountType { get; set; }
    }
}
