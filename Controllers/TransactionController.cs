using Banking_system.DTO_s.TransactionDto_s;
using Banking_system.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // 1- Normal Customer allowed Transactions

        [HttpPost("Deposit")]
        public IActionResult Deposit(TransactionCreateDto trx)
        {
            return Ok();
        }

        [HttpPost("Withdraw")]
        public IActionResult Withdraw(TransactionCreateDto trx)
        {
            return Ok();

        }

        [HttpPost("Transfer")]
        public IActionResult Transfer(TransactionCreateDto trx)
        {
            return Ok();

        }

        [HttpPost("Pay")]
        public IActionResult Pay(TransactionCreateDto trx)
        {
            return Ok();

        }

        [HttpGet("GetTransactionById/{id:int}")]
        public IActionResult GetTransactionById(int id)
        {
            return Ok();
        }


        [HttpGet("GetTransactionsByAccId/{id:int}")]
        public IActionResult GetTransactionsByAccId(int id)
        {
            return Ok();
        }

        [HttpGet("GetTransactionsFromAccByAccId/{id:int}")]
        public IActionResult GetTransactionsFromAccByAccId(int id)
        {
            return Ok();
        }

        [HttpGet("GetTransactionsToAccByAccId/{id:int}")]
        public IActionResult GetTransactionsToAccByAccId(int id)
        {
            return Ok();
        }



        // 2- only Admin allowed Transactions

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllTransactions")]
        public IActionResult GetAllTransactions()
        { 
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteTransaction")]
        public IActionResult DeleteTransaction(int id) 
        {
            return Ok();
        }


    }
}
