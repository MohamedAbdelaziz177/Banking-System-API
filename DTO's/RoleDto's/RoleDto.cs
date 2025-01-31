using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.RoleDto_s
{
    public class RoleDto
    {

        [Required]
        public string? Name { get; set; }

        public string? NormalizedName { get; set; } 

        public string? ConcurrencyStamp { get; set; }
    }
}
