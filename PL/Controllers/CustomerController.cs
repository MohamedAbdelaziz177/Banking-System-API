using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DAL.UnitOfWorkk;
using Banking_system.DTO_s.CustomerDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_system.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var custs = await unitOfWork.CustomersRepo.GetAllAsync();

            List<CustomerReadDto> customersDto = new List<CustomerReadDto>();

            foreach (var cust in custs)
            {
                var custDto = mapper.Map<CustomerReadDto>(cust);

                var user = await userManager.FindByIdAsync(cust.UserId.ToString());

                if (user == null) return BadRequest("User Not found");

                custDto.email = user.Email;
                custDto.address = user.Address;
                custDto.Name = user.UserName;


                customersDto.Add(custDto);
            }

            return Ok(customersDto);
        }


        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {

            bool check = await AllowedTo(id);

            if (!check) return Forbid();


            var cust = await unitOfWork.CustomersRepo.GetByIdAsync(id);

            if (cust == null) return BadRequest("This id is not found");

            var custDto = mapper.Map<CustomerReadDto>(cust);

            var user = await userManager.FindByIdAsync(cust.UserId.ToString());

            if (user == null) return BadRequest("User Not found");

            custDto.email = user.Email;
            custDto.address = user.Address;
            custDto.Name = user.UserName;

            return Ok(custDto);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerCreateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);



            Customer cust = mapper.Map<Customer>(customer);

            await unitOfWork.CustomersRepo.insertAsync(cust);

            return CreatedAtAction(nameof(GetCustomerById), new { id = cust.Id });
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]

        //Needs Modifying
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exisitingCust = await unitOfWork.CustomersRepo.GetByIdAsync(id);

            if (exisitingCust == null) return BadRequest("This id doesn't exist");

            mapper.Map(customer, exisitingCust);

            await unitOfWork.CustomersRepo.updateAsync(id, exisitingCust);

            return Ok(exisitingCust);

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            bool flag = await unitOfWork.CustomersRepo.deleteAsync(id);

            if (!flag) return BadRequest("This id doesn't exist");

            return NoContent();

        }


        // Normal Function helps in logic
        private async Task<bool> AllowedTo(int customerId)
        {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = User.IsInRole("Admin");

            var customer = await unitOfWork.CustomersRepo.GetByIdAsync(customerId);

            if (customer == null) return false;

            var checkUserId = customer.UserId == UserID;

            if (!checkUserId && !isAdmin)
            {
                return false;
            }

            return true;
        }

    }
}
