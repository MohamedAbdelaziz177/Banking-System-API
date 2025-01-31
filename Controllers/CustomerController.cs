using AutoMapper;
using Banking_system.DTO_s.CustomerDto_s;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var custs = await unitOfWork.CustomersRepo.GetAllAsync();

            List<CustomerReadDto> customersDto = new List<CustomerReadDto>();

            foreach (var cust in custs)
            {
                customersDto.Add(mapper.Map<CustomerReadDto>(cust));
            }

            return Ok(customersDto);
        }


        [HttpGet("GetCustomerById{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id) 
        {
            var cust = await unitOfWork.CustomersRepo.GetByIdAsync(id); 

            if (cust == null) return NotFound("This id is not found");

            var custDto = mapper.Map<CustomerReadDto>(cust);

            return Ok(custDto);
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerCreateDto customer)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            Customer cust = mapper.Map<Customer>(customer);
            await unitOfWork.CustomersRepo.insertAsync(cust);

            return CreatedAtAction(nameof(GetCustomerById), new { id = cust.Id });
        }

        [HttpPut("UpdateCustomer{id:int}")]
        
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exisitingCust = await unitOfWork.CustomersRepo.GetByIdAsync(id);

            if (exisitingCust == null) return NotFound("This id doesn't exist");

            mapper.Map(customer, exisitingCust);

            await unitOfWork.CustomersRepo.updateAsync(id, exisitingCust);

            return Ok(exisitingCust);

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteCustomer{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            bool flag =  await unitOfWork.CustomersRepo.deleteAsync(id);

            if (!flag) return NotFound("This id doesn't exist");

            return NoContent();

        }

    }
}
