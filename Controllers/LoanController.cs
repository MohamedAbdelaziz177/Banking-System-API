using Banking_system.DTO_s.LoanDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        [HttpGet("GetAllLoans")]
        public IActionResult GetAllLoans()
        {
            return Ok();
        }


        
        [HttpGet("GelLoanById/{id:int}")]
        public IActionResult GetLoanById(int id)
        {
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateLoan")]
        public IActionResult CreateLoan(LoanCreateDto loan) 
        {
            return Ok();
        }


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateLoan/{id:int}")]
        public IActionResult UpdateLoan(int id, LoanUpdateDto loan)
        {
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteLoan/{id:int}")]
        public IActionResult DeleteLoan(int id) 
        {
            return Ok();
        }

        
        [HttpGet("GetLoanByCustomerId/{id:int}")]
        public IActionResult GetLoanByCustomerId(int id) 
        {
            return Ok();
        }
    }
}
