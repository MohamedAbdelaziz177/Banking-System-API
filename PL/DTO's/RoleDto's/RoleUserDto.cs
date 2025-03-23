using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.RoleDto_s
{
    public class RoleUserDto
    {
        [Required]
        public string userName {  get; set; }

        [Required]
        [AllowedValues("Admin", "User")]
        public string Role {  get; set; }
    }
}
