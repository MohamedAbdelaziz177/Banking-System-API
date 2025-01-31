using AutoMapper;
using Banking_system.DTO_s.TransactionDto_s;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TransactionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // 1- Normal Customer allowed Transactions

        [HttpGet("Deposit/{id:int}")]
        public async Task<IActionResult> Deposit(TransactionCreateDto trxDto)
        {
            var transaction = unitOfWork.BeginTransaction();
            try
            {
                await unitOfWork.AccountsRepo.Deposit((int)trxDto.ToAccountId, trxDto.amount);

                Transaction trxx = mapper.Map<Transaction>(trxDto);

                await unitOfWork.TransactionsRepo.insertAsync(trxx);

                await transaction.CommitAsync();

                return Ok(trxDto);
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
            }

            return Ok();
            return BadRequest();

        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(TransactionCreateDto trxDto)
        {
            var transaction = unitOfWork.BeginTransaction();
            try
            {
                bool f = await unitOfWork.AccountsRepo.Withdraw((int)trxDto.FromAccountId, trxDto.amount);

                if(!f) return BadRequest();


                Transaction trxx = mapper.Map<Transaction>(trxDto);

                await unitOfWork.TransactionsRepo.insertAsync(trxx);

                await transaction.CommitAsync();

                return Ok(trxDto);

            }
            catch {
                transaction.Rollback();
                return BadRequest();

            }

        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(TransactionCreateDto trx)
        {
            var transaction = unitOfWork.BeginTransaction();
            try
            {
                bool f = await unitOfWork.AccountsRepo.Withdraw((int)trx.FromAccountId, trx.amount);

                if(!f) return BadRequest();

                await unitOfWork.AccountsRepo.Deposit((int)trx.ToAccountId, trx.amount);

                TransactionCreateDto transDto = new TransactionCreateDto();
               

                Transaction trxx = mapper.Map<Transaction>(trx);
                await unitOfWork.TransactionsRepo.insertAsync(trxx);


                transaction.Commit();

                return Ok(trx);
            }
            catch { 
                transaction.Rollback(); 
                return BadRequest();
            }

        }

       

        [HttpGet("GetTransactionById/{id:int}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var trx = await unitOfWork.TransactionsRepo.GetByIdAsync(id);

            if (trx == null) return NotFound("Id doesn't exist");

            var trxDto = mapper.Map<TransactionReadDto>(trx);

            return Ok(trxDto);
        }


        [HttpGet("GetTransactionsByAccId/{id:int}")]
        public async Task<IActionResult> GetTransactionsByAccId(int id)
        {
            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(allTrx);
        }

        [HttpGet("GetTransactionsFromAccByAccId/{id:int}")]
        public async Task<IActionResult> GetTransactionsFromAccByAccId(int id)
        {
            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByFromAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(allTrx);
        }

        [HttpGet("GetTransactionsToAccByAccId/{id:int}")]
        public async Task<IActionResult> GetTransactionsToAccByAccId(int id)
        {
            var allTrx = await unitOfWork.TransactionsRepo.GetAllTrxByToAccId(id);
            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in allTrx)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(allTrx);
        }



        // 2- only Admin allowed Transactions

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var Trans = await unitOfWork.TransactionsRepo.GetAllAsync();

            var TransDto = new List<TransactionReadDto>();

            foreach (var trx in Trans)
                TransDto.Add(mapper.Map<TransactionReadDto>(trx));

            return Ok(TransDto);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int id) 
        {
            bool flg = await unitOfWork.TransactionsRepo.deleteAsync(id);
            if (!flg) return NotFound("id doesn't exist");

            return NoContent();
        }


    }
}
