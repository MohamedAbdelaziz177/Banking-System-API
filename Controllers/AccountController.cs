using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.AccountDto_s;
using Banking_system.Enums.Account;
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
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await unitOfWork.AccountsRepo.GetAllAsync();

            var accountsDto = new List<AccountReadDto>();

            foreach (var account in accounts)
            {
                accountsDto.Add(mapper.Map<AccountReadDto>(account));
            }
            return Ok(accountsDto);
        }


        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            bool check = await AllowedTo(id);

            if (!check) return Forbid();

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if(account == null) return NotFound("there is no account with this id");

            var accountDto = mapper.Map<AccountReadDto>(account);

            return Ok(accountDto);
            
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto acc)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var account = mapper.Map<Account>(acc);
            await unitOfWork.AccountsRepo.insertAsync(account);

            return CreatedAtAction("GetAccountById", new {id = account.Id}, account);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] AccountUpdateDto acc)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingAcc = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (existingAcc == null) return NotFound("this id doesn't exist");

            mapper.Map(acc, existingAcc);


            await unitOfWork.AccountsRepo.updateAsync(id, existingAcc);

            return Ok(existingAcc);
        }

        
        [HttpPut("ChangeStatus/{id:int}")]
        public async Task<IActionResult> ChangeAccountStatus([FromRoute] int id, [FromBody] ChangeAccStatusDto acc)
        {
            bool check = await AllowedTo(id);

            if (!check) return Forbid();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound("there is no account with this id");

            account.accountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), acc.accountStatus);
            await unitOfWork.Complete();

            return Ok();

        }

        
        [HttpPut("ChangeType/{id:int}")]
        public async Task<IActionResult> ChangeAccountType([FromRoute] int id, [FromBody] ChangeAccTypeDto acc)
        {
            bool check = await AllowedTo(id);

            if (!check) return Forbid();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound("there is no account with this id");

            account.accountType = (AccountType)Enum.Parse(typeof(AccountType), acc.accountType);

            await unitOfWork.Complete();

            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id) 
        {
            bool flag = await unitOfWork.AccountsRepo.deleteAsync(id);

            if (!flag) return NotFound("This id doesn't exist");

            return NoContent();
        }

        // Normal Functions serving the logic
        private async Task<bool> AllowedTo(int accId)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = User.IsInRole("admin");

            var acc = await unitOfWork.AccountsRepo.GetByIdAsync(accId);
            var AccOwner = await unitOfWork.CustomersRepo.GetByIdAsync(acc.customerId);
            var AccUserId = AccOwner.UserId;

            var checkUserId = (AccUserId == UserID);

            if (!checkUserId && !isAdmin)
                return false;


            return true;
        }

    }
}
