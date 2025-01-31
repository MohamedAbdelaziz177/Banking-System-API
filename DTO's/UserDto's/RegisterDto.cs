using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s
{
    public class RegisterDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]

        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string password { get; set; }

        [Compare("password")]
        [Required]
        public string confirmPassword {  get; set; }

        
        public string Address { get; set; }


    }
}
