using Banking_system.DTO_s.Custom_Validations;
using Banking_system.Enums.Account;
using Banking_system.Enums.Card;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AccountDto_s
{
    public class AccountCreateDto
    {

        public int CustomerId { get; set; }

        [EnumDataType(typeof(AccountType), ErrorMessage = "only savings - checking - business")]
        public string AccountType { get; set; }

       // public string accountStatus { get; set; }
        public decimal balance { get; set; }
    }
}
