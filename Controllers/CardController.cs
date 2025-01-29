using Banking_system.DTO_s.CardDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpGet("GetAllCards")]
        public IActionResult GetAllCards()
        {
            return Ok();
        }

        [HttpGet("GetCardById/{id:int}")]
        public IActionResult GetCardById(int id)
        {
            return Ok();
        }

        [HttpPost("CreateCard")]
        public IActionResult CreateCard(CardCreateDto card)
        {
            return Ok();
        }

        [HttpPut("UpdateCard/{id:int}")]
        public IActionResult UpdateCard(int id, CardUpdateDto card) 
        {
            return Ok();
        }

        [HttpPut("Deposit/{id:int}")]
        public IActionResult Deposit(int id)
        {
            return Ok();
        }

        [HttpPut("Withdraw/{id:int}")]
        public IActionResult Withdraw(int id)
        {
            return Ok();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id:int}")]
        public IActionResult DeleteCard(int id)
        {
            return Ok();
        }

        private string GenerateCardNumber()
        {
            return "";
        }
    }
}
