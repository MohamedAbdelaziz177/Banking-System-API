using Banking_system.DTO_s.AccountDto_s;
using Banking_system.Enums.Account;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await unitOfWork.AccountsRepo.GetAllAsync();
            return Ok(accounts);
        }

        [HttpGet("GetAccountById/{id:int}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if(account != null)
            return Ok(account);

            else return NotFound();
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] AccountCreateDto acc)
        {
            return Ok();
        }

        [HttpPut("UpdateAccount/{id:int}")]
        public IActionResult UpdateAccount([FromRoute] int id, [FromBody] AccountUpdateDto acc)
        {
            return Ok();

        }

        [HttpPut("ChangeAccountStatus/{id:int}")]
        public async Task<IActionResult> ChangeAccountStatus([FromRoute] int id, [FromBody] ChangeAccStatusDto acc)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound();

            account.accountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), acc.accountStatus);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetAccountById), new { id });

        }

        [HttpPut("ChangeAccountType/{id:int}")]
        public async Task<IActionResult> ChangeAccountType([FromRoute] int id, [FromBody] ChangeAccTypeDto acc)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound();

            account.accountType = (AccountType)Enum.Parse(typeof(AccountType), acc.accountType);

            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetAccountById), new { id });
              
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id) 
        {
            await unitOfWork.AccountsRepo.deleteAsync(id);

            return Ok();
        }
        
    }
}
