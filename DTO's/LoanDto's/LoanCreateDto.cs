using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.LoanDto_s
{
    public class LoanCreateDto
    {
        public int CustomerId { get; set; }
        public decimal amount { get; set; }

        [Range(10, 50)]
        public decimal InterestRate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
