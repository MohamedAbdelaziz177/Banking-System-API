using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.AuthDto
{
    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string token { get; set; }
    }
}
