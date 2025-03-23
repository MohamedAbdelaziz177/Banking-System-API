using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DAL.UnitOfWorkk;
using Banking_system.DTO_s.TransactionDto_s;
using Banking_system.Services.AuthService_d;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;
using System.Security.Claims;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMailService mailService;

        public TransactionController(IUnitOfWork unitOfWork,
                                     IMapper mapper,
                                     IMailService mailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.mailService = mailService;
        }

        // 1- Normal Customer allowed Transactions

    
        [HttpPost("Deposit")]
        [Authorize]

        public async Task<IActionResult> Deposit(TransactionCreateDto trxDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);


            bool check = await AllowedTo((int)trxDto.ToAccountId);

            if (!check) return Unauthorized();

            var transaction = unitOfWork.BeginTransaction();
            try
            {
                await unitOfWork.AccountsRepo.Deposit((int)trxDto.ToAccountId, trxDto.amount);

               // Console.WriteLine("-----valid");

                Transaction trxx = mapper.Map<Transaction>(trxDto);

               // Console.WriteLine("-----valid");


                await unitOfWork.TransactionsRepo.insertAsync(trxx);


                await mailService.SendMailAsync(User.FindFirstValue(ClaimTypes.Email),
                                          "Deposit Assurance",
                                          $"<h2>You deposited {trxDto.amount} to ur account</h2>"
                                          );

              //  Console.WriteLine("-----valid");


                await transaction.CommitAsync();

                return Ok(trxDto);
            }
            catch (Exception ex) 
            {
                transaction.Rollback();

                Console.WriteLine(ex.Message.ToString());

                return BadRequest();
            }

            return Ok();

        }

        
        [HttpPost("Withdraw")]
        [Authorize]

        public async Task<IActionResult> Withdraw(TransactionCreateDto trxDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Console.WriteLine(trxDto.FromAccountId + "assssAS");
            bool check = await AllowedTo((int)trxDto.FromAccountId);

            if (!check) return Unauthorized();

            using var transaction = unitOfWork.BeginTransaction();
            try
            {
                bool f = await unitOfWork.AccountsRepo.Withdraw((int)trxDto.FromAccountId, trxDto.amount);

                if(!f) return BadRequest("Ur total balance is less than the requested amount");


                Transaction trxx = mapper.Map<Transaction>(trxDto);

                await unitOfWork.TransactionsRepo.insertAsync(trxx);

                await mailService.SendMailAsync(User.FindFirstValue(ClaimTypes.Email),
                                          "Withdrawal Assurance",
                                          $"<h2>You withdrawed {trxDto.amount} from ur account</h2>"
                                          );

                await transaction.CommitAsync();

                return Ok(trxDto);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return BadRequest();
            }

        }

        [HttpPost("Transfer")]
        [Authorize]
        public async Task<IActionResult> Transfer(TransactionCreateDto trx)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            bool check = await AllowedTo((int)trx.FromAccountId);
            if (!check) return Unauthorized();

            using var transaction = unitOfWork.BeginTransaction();
            try
            {
                bool f = await unitOfWork.AccountsRepo.Withdraw((int)trx.FromAccountId, trx.amount);

                if(!f) return BadRequest();

                await unitOfWork.AccountsRepo.Deposit((int)trx.ToAccountId, trx.amount);

                TransactionCreateDto transDto = new TransactionCreateDto();
               

                Transaction trxx = mapper.Map<Transaction>(trx);
                await unitOfWork.TransactionsRepo.insertAsync(trxx);

                await mailService.SendMailAsync(User.FindFirstValue(ClaimTypes.Email),
                                          "Transfer Assurance",
                                          $"<h2>You transfered {trx.amount} to account {trx.ToAccountId}</h2>"
                                          );

                transaction.Commit();

                return Ok(trx);
            }
            catch { 
                transaction.Rollback(); 
                return BadRequest();
            }

        }

       

        [HttpGet("GetTransactionById/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionById(int id)
        {
           

            var trx = await unitOfWork.TransactionsRepo.GetByIdAsync(id);

            if (trx == null) return NotFound("Id doesn't exist");

            var trxDto = mapper.Map<TransactionReadDto>(trx);

            return Ok(trxDto);
        }


        [HttpGet("GetTransactionsByAccId/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionsByAccId(int id)
        {
            bool check = await AllowedTo(id);
            if (!check) return Forbid();

            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(TransDto);
        }

        [HttpGet("GetTransactionsFromAccByAccId/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionsFromAccByAccId(int id)
        {
            bool check = await AllowedTo(id);
            if (!check) return Forbid();

            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByFromAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(TransDto);
        }

        [HttpGet("GetTransactionsToAccByAccId/{id:int}")]
        [Authorize]

        public async Task<IActionResult> GetTransactionsToAccByAccId(int id)
        {
            bool check = await AllowedTo(id);
            if (!check) return Forbid();

            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByToAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(TransDto);
        }



        // 2- only Admin allowed Transactions

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var Trans = await unitOfWork.TransactionsRepo.GetAllAsync();

            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in Trans)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(TransDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int id) 
        {
            bool flg = await unitOfWork.TransactionsRepo.deleteAsync(id);
            if (!flg) return NotFound("id doesn't exist");

            return NoContent();
        }

        // Normal Functions serving the logic
       

        private async Task<bool> AllowedTo(int id)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isAdmin = User.IsInRole("Admin");

            var acc = await unitOfWork.AccountsRepo.GetByIdAsync(id);
            var AccOwner = await unitOfWork.CustomersRepo.GetByIdAsync(acc.customerId);
            var AccUserId = AccOwner.UserId;

            var checkUserId = (AccUserId == UserID);

            if (!checkUserId && !isAdmin)
                return false;


            return true;

        }
    }
}
