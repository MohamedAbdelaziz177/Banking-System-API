using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Banking_system.DTO_s.AuthDto
{
    public class RegisterDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }


        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        [Required, StringLength(128)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
