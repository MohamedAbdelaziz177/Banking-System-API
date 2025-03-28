﻿using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.RoleDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Banking_system.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {

        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
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

        [HttpPost("AssignToUser")]
        public async Task<IActionResult> AssignRoleToUser(RoleUserDto roleUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(roleUserDto.userName);

            if (user == null) return BadRequest("User Not Found");

            var res = await userManager.AddToRoleAsync(user, roleUserDto.Role);

            if (res.Succeeded) return Ok("Role Assigned to user successfully");

            return BadRequest("Failed to assign user, Pleaze try again");
        }


    }
}
