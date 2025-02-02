using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.LoanDto_s;
using Banking_system.Enums.Loan;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoanController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllLoans")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await unitOfWork.LoansRepo.GetAllAsync();

            List<LoanReadDto> loansDto = new List<LoanReadDto>();

            foreach (var loan in loans) 
            {
                loansDto.Add(mapper.Map<LoanReadDto>(loan));
            }

            return Ok(loansDto);

        }

        [HttpGet("GelLoanById/{id:int}")]
        public async  Task<IActionResult> GetLoanById(int id)
        {
            bool check = await AllowedTo(id);

            if (!check) return Forbid();

            var loan = await unitOfWork.LoansRepo.GetByIdAsync(id);

            if(loan == null) return NotFound("id doesn't exist");

            var loanDto = mapper.Map<LoanReadDto>(loan);

            return Ok(loanDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan(LoanCreateDto loanDto) 
        {
            if (!ModelState.IsValid) return BadRequest();

            var loan = mapper.Map<Loan>(loanDto);

            await unitOfWork.LoansRepo.insertAsync(loan);
            
            return CreatedAtAction(nameof(GetLoanById), new {id = loan.Id});
        }


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateLoan/{id:int}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanUpdateDto loanDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingLoan = await unitOfWork.LoansRepo.GetByIdAsync(id);

            if (existingLoan == null) return NotFound("this id doesn't exist");

            mapper.Map(loanDto, existingLoan);


            await unitOfWork.LoansRepo.updateAsync(id, existingLoan);

            return Ok(existingLoan);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteLoan/{id:int}")]
        public async Task<IActionResult> DeleteLoan(int id) 
        {
            bool flag = await unitOfWork.LoansRepo.deleteAsync(id);

            if(!flag) return NotFound("This id doesn't exist");  

            return NoContent();
        }

        
        [HttpGet("GetLoanByCustomerId/{id:int}")]
        public async Task<IActionResult> GetLoanByCustomerId(int id) 
        {
           

            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.customerId == id);

            // if(filteredLoans == null) return Ok("No loans taken by this customer");

            return Ok(filteredLoans);
        }

        [HttpGet("GetPaidLoans")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPaidLoans()
        {
            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.loanStatus == LoanStatus.paid);

            return Ok(filteredLoans);
        }


        [HttpGet("GetUnpaidLoans")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUnpaidLoans()
        {
            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.loanStatus == LoanStatus.active);

            return Ok(filteredLoans);
        }

        // Normal Functions serving the logic
        private async Task<bool> AllowedTo(int loanId)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = User.IsInRole("admin");

            var loan = await unitOfWork.LoansRepo.GetByIdAsync(loanId);
            var loanOwner = await unitOfWork.CustomersRepo.GetByIdAsync(loan.customerId);
            var loanOwnerId = loanOwner.UserId;

            var checkUserId = (loanOwnerId == UserID);

            if (!checkUserId && !isAdmin)
                return false;


            return true;
        }

    }
}
