using Banking_system.DAL.Enums.Account;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class AccountUpdateDto
    {
        
        [EnumDataType(typeof(AccountStatus), ErrorMessage = "only active - inactive - closed")]
        public string AccountStatus { get; set; }

        [EnumDataType(typeof(AccountType), ErrorMessage = "only savings - checking - business")]
        public string AccountType { get; set; }
        public decimal balance { get; set; }
    }
}
