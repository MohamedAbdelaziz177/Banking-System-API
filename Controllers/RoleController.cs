using AutoMapper;
using Banking_system.DTO_s.RoleDto_s;
using Banking_system.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var role = roleManager.FindByIdAsync(id.ToString());
            RoleDto mappedRole =  mapper.Map<RoleDto>(role);

            return Ok(mappedRole);
            
        }


        public async Task<IActionResult> AddRole([FromBody] RoleDto role)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

           var mappedRole = mapper.Map<Role>(role);
           var res = await roleManager.CreateAsync(mappedRole);

            if (res.Succeeded) 
            {
                return CreatedAtAction("GetRoleById", new {id = mappedRole.Id});
            }

            return BadRequest(ModelState);
           
        }

        public async Task<IActionResult> UpdateRole([FromBody] RoleDto role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mappedRole = mapper.Map<Role>(role);

            var res = await roleManager.UpdateAsync(mappedRole);


            if(res.Succeeded)
            return Ok(mappedRole);

            return BadRequest(ModelState);

        }

    }
}
