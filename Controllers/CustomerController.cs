using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var custs = await unitOfWork.CustomersRepo.GetAllAsync();
            return Ok(custs);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id) 
        {
            var cust = await unitOfWork.CustomersRepo.GetByIdAsync(id);
            return Ok(cust);
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer([FromBody] Customer customer)
        {
            await unitOfWork.CustomersRepo.insertAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id });
        }

    }
}
