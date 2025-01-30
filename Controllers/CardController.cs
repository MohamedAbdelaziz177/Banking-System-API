using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.CardDto_s;
using Banking_system.Enums.Account;
using Banking_system.Enums.Card;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAllCards")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllCards()
        {
            var allCards = await unitOfWork.CardsRepo.GetAllAsync();

            var allCardsDto = new List<CardReadDto>();

            foreach(var card in allCards) 
                allCardsDto.Add(mapper.Map<CardReadDto>(card));

            return Ok(allCardsDto);
        }

        [HttpGet("GetCardById/{id:int}")]
        public IActionResult GetCardById(int id)
        {
            var card = unitOfWork.CardsRepo.GetByIdAsync(id);

            var cardDto = mapper.Map<CardReadDto>(card);

            if (card == null) return NotFound("This id doesn't exist");

            return Ok(cardDto);
        }

        [Authorize(Roles ="admin")]
        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCard(CardCreateDto card)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var mappedCard = mapper.Map<Card>(card);

            mappedCard.cardNumber = GenerateCardNumber();
            mappedCard.ExpiryDate = DateTime.Now.AddYears(2);
            mappedCard.cardStatus = CardStatus.active;

            await unitOfWork.CardsRepo.insertAsync(mappedCard);

            return Ok(mappedCard);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("UpdateCard/{id:int}")]
        public async Task<IActionResult> UpdateCard(int id, CardUpdateDto card) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mappedCard = mapper.Map<Card>(card);
            await unitOfWork.CardsRepo.updateAsync(id, mappedCard);

            return CreatedAtAction("GetCardById", new { id = id });
        }

        [HttpPut("ChangeCardStattus/{id:int}")]
        public async Task<IActionResult> ChangeCardStatus(int id, [FromQuery]string Status)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            
            CardStatus accSt = (CardStatus)Enum.Parse(typeof(CardStatus), Status);

            card.cardStatus = accSt;
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetCardById), new { id = id });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("SetExpiryDate/{id:int}")]
        public async Task<IActionResult> SetExpiryDate(int id)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);
            card.ExpiryDate = DateTime.Now.AddYears(2);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetCardById), new{ id = id});
        }

        [Authorize(Roles = "admin")]
        [HttpPut("CheckExpiring/{id:int}")]
        public async Task<IActionResult> CheckExpiring()
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
        public async Task<IActionResult> Deposit(int id, [FromQuery]decimal amount)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (card == null) return NotFound("This id doesn't exist");

            if (card.cardStatus != CardStatus.active) return BadRequest("Transactions are not allowed");

            card.amount += amount;
            await unitOfWork.Complete();

            return CreatedAtAction("GetCardById", new { id = id });
        }

        [HttpPut("Withdraw/{id:int}")]
        public async Task<IActionResult> Withdraw(int id, [FromQuery] decimal amount)
        {
            var card = await unitOfWork.CardsRepo.GetByIdAsync(id);

            if (card == null) return NotFound("This id doesn't exist");
            if (card.cardStatus != CardStatus.active) return BadRequest("Transactions are not allowed");

            card.amount -= amount;
            await unitOfWork.Complete();

            return CreatedAtAction("GetCardById", new {id = id});
            
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            bool flag = await unitOfWork.CardsRepo.deleteAsync(id);

            if (!flag) return NotFound("id deosn't exist");

            return NoContent();
        }

        private string GenerateCardNumber()
        {
            // Simple Random .. can work
            return "4000-" + new Random().Next(1000, 9999) + "-" + new Random().Next(1000, 9999) + "-0001";
        }
    }
}
