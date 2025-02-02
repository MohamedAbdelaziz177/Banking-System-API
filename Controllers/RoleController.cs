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
    //[Authorize]

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

        //[HttpGet("GetAllRoles")]



        
        [HttpGet("GetRole/{id:int}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());

            if (role == null) return NotFound("This id doesn't exist");

            RoleDto mappedRole = mapper.Map<RoleDto>(role);
        
            return Ok(role);
        
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
        
            var mappedRole = mapper.Map<Role>(role);
            var res = await roleManager.CreateAsync(mappedRole);
        
            if (res.Succeeded)
            {
                return CreatedAtAction("GetRoleById", new { id = mappedRole.Id });
            }
        
            return BadRequest(ModelState);
        
        }

        [HttpPost("UpdateRole/{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
        
          //  var mappedRole = mapper.Map<Role>(role);

            var existingRole = await roleManager.FindByIdAsync(id.ToString());

            if (existingRole == null) return NotFound("This id doesn't exist");

            mapper.Map(role, existingRole);

            var res = await roleManager.UpdateAsync(existingRole);
        
        
            if (res.Succeeded)
                return CreatedAtAction("GetRoleById", new { id = existingRole.Id });

            return BadRequest(ModelState);
        
        }


    }
}
