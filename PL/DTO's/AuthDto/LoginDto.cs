using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AuthDto
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
