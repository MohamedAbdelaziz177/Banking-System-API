using AutoMapper;
using Banking_system.DAL.Enums.Loan;
using Banking_system.DAL.Model;
using Banking_system.DAL.UnitOfWorkk;
using Banking_system.DTO_s;
using Banking_system.DTO_s.LoanDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_system.PL.Controllers
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

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
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

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            bool check = await AllowedTo(id);

            if (!check) return Unauthorized();

            var loan = await unitOfWork.LoansRepo.GetByIdAsync(id);

            if (loan == null) return BadRequest("id doesn't exist");

            var loanDto = mapper.Map<LoanReadDto>(loan);

            return Ok(loanDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateLoan(LoanCreateDto loanDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var loan = mapper.Map<Loan>(loanDto);

            await unitOfWork.LoansRepo.insertAsync(loan);

            var loanReadDto = mapper.Map<LoanReadDto>(loan);

            return CreatedAtAction(nameof(GetLoanById), new { id = loan.Id }, loanReadDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanUpdateDto loanDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingLoan = await unitOfWork.LoansRepo.GetByIdAsync(id);

            if (existingLoan == null) return BadRequest("this id doesn't exist");

            mapper.Map(loanDto, existingLoan);


            await unitOfWork.LoansRepo.updateAsync(id, existingLoan);

            return Ok(mapper.Map<LoanReadDto>(existingLoan));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            bool flag = await unitOfWork.LoansRepo.deleteAsync(id);

            if (!flag) return NotFound("This id doesn't exist");

            return NoContent();
        }


        [HttpGet("GetByCustomerId/{id:int}")]
        public async Task<IActionResult> GetLoanByCustomerId(int id)
        {


            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.customerId == id);

            var loanDtos = new List<LoanReadDto>();

            foreach (var loan in filteredLoans)
            {
                loanDtos.Add(mapper.Map<LoanReadDto>(loan));
            }

            return Ok(loanDtos);
        }

        [HttpGet("GetPaidLoans")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaidLoans()
        {
            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.loanStatus == LoanStatus.paid);

            var loanDtos = new List<LoanReadDto>();

            foreach (var loan in filteredLoans)
            {
                loanDtos.Add(mapper.Map<LoanReadDto>(loan));
            }

            return Ok(loanDtos);
        }


        [HttpGet("GetUnpaidLoans")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUnpaidLoans()
        {
            var allLoans = await unitOfWork.LoansRepo.GetAllAsync();
            var filteredLoans = allLoans.Where(e => e.loanStatus == LoanStatus.active);

            var loanDtos = new List<LoanReadDto>();

            foreach (var loan in filteredLoans)
            {
                loanDtos.Add(mapper.Map<LoanReadDto>(loan));
            }

            return Ok(loanDtos);
        }

        // Normal Functions serving the logic
        private async Task<bool> AllowedTo(int loanId)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isAdmin = User.IsInRole("Admin");

            var loan = await unitOfWork.LoansRepo.GetByIdAsync(loanId);

            if (loan == null) return false;

            var loanOwner = await unitOfWork.CustomersRepo.GetByIdAsync(loan.customerId);

            if (loanOwner == null) return false;

            var loanOwnerId = loanOwner.UserId;

            var checkUserId = loanOwnerId == UserID;

            if (!checkUserId && !isAdmin)
                return false;


            return true;
        }

    }
}
