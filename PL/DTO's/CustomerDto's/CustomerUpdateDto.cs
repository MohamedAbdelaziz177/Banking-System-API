using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.CustomerDto_s
{
    public class CustomerUpdateDto
    {
        // public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string address { get; set; }
    }
}
