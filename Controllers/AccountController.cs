using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.AccountDto_s;
using Banking_system.Enums.Account;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAccountById/{id:int}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if(account == null) return NotFound("there is no account with this id");

            var accountDto = mapper.Map<AccountReadDto>(account);

            return Ok(accountDto);
            
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto acc)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var account = mapper.Map<Account>(acc);
            await unitOfWork.AccountsRepo.insertAsync(account);

            return CreatedAtAction("GetAccountById", new {id = account.Id});
        }

        [HttpPut("UpdateAccount/{id:int}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] AccountUpdateDto acc)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingAcc = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (existingAcc == null) return NotFound("this id doesn't exist");

            mapper.Map(acc, existingAcc);


            await unitOfWork.AccountsRepo.updateAsync(id, existingAcc);

            return Ok(existingAcc);
        }

        [HttpPut("ChangeAccountStatus/{id:int}")]
        public async Task<IActionResult> ChangeAccountStatus([FromRoute] int id, [FromBody] ChangeAccStatusDto acc)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound("there is no account with this id");

            account.accountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), acc.accountStatus);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetAccountById), new { id });

        }

        [HttpPut("ChangeAccountType/{id:int}")]
        public async Task<IActionResult> ChangeAccountType([FromRoute] int id, [FromBody] ChangeAccTypeDto acc)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = await unitOfWork.AccountsRepo.GetByIdAsync(id);

            if (account == null) return NotFound("there is no account with this id");

            account.accountType = (AccountType)Enum.Parse(typeof(AccountType), acc.accountType);

            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetAccountById), new { id });
              
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id) 
        {
            bool flag = await unitOfWork.AccountsRepo.deleteAsync(id);

            if (!flag) return NotFound("This id doesn't exist");

            return NoContent();
        }
        
    }
}
