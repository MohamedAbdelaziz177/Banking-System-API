using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DAL.UnitOfWorkk;
using Banking_system.DTO_s;
using Banking_system.DTO_s.CardDto_s;
using Banking_system.Enums.Account;
using Banking_system.Enums.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CardController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCards()
        {
            var allCards = await unitOfWork.CardsRepo.GetAllAsync();

            var allCardsDto = new List<CardReadDto>();

            foreach(var card in allCards) 
                allCardsDto.Add(mapper.Map<CardReadDto>(card));

            return Ok(allCardsDto);
        }



        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            bool check = await AllowedTo(id);

            if(!check) return Unauthorized();


            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (card == null) return BadRequest("This id doesn't exist");

            var cardDto = mapper.Map<CardReadDto>(card);

            return Ok(cardDto);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCard(CardCreateDto card)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var mappedCard = mapper.Map<Card>(card);

            mappedCard.cardNumber = GenerateCardNumber();
            mappedCard.ExpiryDate = DateTime.Now.AddYears(2);
            mappedCard.cardStatus = CardStatus.active;

            await unitOfWork.CardsRepo.insertAsync(mappedCard);

            return CreatedAtAction("GetCardById", new {id = mappedCard.Id}, mappedCard);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateCard(int id, CardUpdateDto card) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCard = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (existingCard == null) return NotFound("this id doesn't exist");

             mapper.Map(card, existingCard);
        
            await unitOfWork.CardsRepo.updateAsync(id, existingCard);

            return Ok(existingCard);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeStatus/{id:int}")]
        public async Task<IActionResult> ChangeCardStatus(int id, [FromQuery]string Status)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            
            CardStatus accSt = (CardStatus)Enum.Parse(typeof(CardStatus), Status);

            card.cardStatus = accSt;
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetCardById), new { id = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("SetExpiryDate/{id:int}")]
        public async Task<IActionResult> SetExpiryDate(int id)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);
            card.ExpiryDate = DateTime.Now.AddYears(2);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetCardById), new{ id = id});
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Expire/{id:int}")]
        public async Task<IActionResult> ExpireCards()
        {
            var cards = await unitOfWork.CardsRepo.GetAllAsync();
            var expiredCards = new List<Card>();

            foreach (var card in cards)
                if (card.ExpiryDate == DateTime.Now)
                {
                    card.cardStatus = CardStatus.expired;
                    expiredCards.Add(card);
                }

            await unitOfWork.Complete();

            return Ok(expiredCards);
            
        }


        [HttpPut("Deposit/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Deposit(int id, [FromQuery]decimal amount)
        {
            bool check = await AllowedTo(id);

            if (!check) return Unauthorized();


            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (card == null) return NotFound("This id doesn't exist");

            if (card.cardStatus != CardStatus.active) return BadRequest("Transactions are not allowed");

            card.amount += amount;
            await unitOfWork.Complete();

            return Ok(card.amount);
        }

        [HttpPut("Withdraw/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Withdraw(int id, [FromQuery] decimal amount)
        {
            bool check = await AllowedTo(id);

            if (!check) return Unauthorized();

            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (card == null) return NotFound("This id doesn't exist");
            if (card.cardStatus != CardStatus.active) return BadRequest("Transactions are not allowed");

            card.amount -= amount;
            await unitOfWork.Complete();

            return Ok(card.amount);
            
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            bool flag = await unitOfWork.CardsRepo.deleteAsync(id);

            if (!flag) return NotFound("id deosn't exist");

            return NoContent();
        }


        // Normal Functions serving the logic
        private async Task<bool> AllowedTo(int cardId)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            

            var isAdmin = User.IsInRole("Admin");

            var card = await unitOfWork.CardsRepo.GetByIdAsync(cardId);

            if (card == null) return false;

            var CardOwner = await unitOfWork.CustomersRepo.GetByIdAsync(card.customerId);

            if (CardOwner == null) return false;

            var CardUserId = CardOwner.UserId;

            var checkUserId = (CardUserId == UserID);

            if (!checkUserId && !isAdmin)
                return false;
            

            return true;
        }

        private string GenerateCardNumber()
        {
            // Simple Random .. can work
            return "4000-" + new Random().Next(1000, 9999) + "-" + new Random().Next(1000, 9999) + "-0001";
        }

      //[HttpGet("Get")]
      //[Authorize]
      //public IActionResult GetNothing()
      //{
      //    Console.WriteLine("Im inside");
      //    return Ok();
      //}
    }
}
