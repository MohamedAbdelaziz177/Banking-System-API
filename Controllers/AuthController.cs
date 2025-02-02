using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public AuthController(UserManager<AppUser> userManager, IConfiguration config, IMapper mapper)
        {
            this.userManager = userManager;
            this.config = config;
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            if (ModelState.IsValid) 
            {
                AppUser AppUser = mapper.Map<AppUser>(user);

                IdentityResult res = await userManager.CreateAsync(AppUser, user.password);

                if (res.Succeeded) 
                {
                    var resII = await userManager.AddToRoleAsync(AppUser, "user");

                    if (resII.Succeeded)
                    return Ok(res);
                }

            }

            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto user)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            AppUser appUser = await userManager.FindByNameAsync(user.userName);

            if (appUser == null)
            {
                return BadRequest();
            }

            bool found = await userManager.CheckPasswordAsync(appUser, user.password);

            if (!found) 
            {
                return BadRequest();
            }

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, appUser.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }


            SecurityKey securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:secret"]));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(

                issuer: config["JWT:issuer"],
                audience: config["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCred

            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string tokenCreated = handler.WriteToken(token);

            return Ok(new
            {
                token = tokenCreated,
                expr = DateTime.Now.AddHours(1)
            });


          
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddNewAdmin")]
        public async Task<IActionResult> AddAdmin(RegisterDto user)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            AppUser AppUser = mapper.Map<AppUser>(user);

            IdentityResult res = await userManager.CreateAsync(AppUser, user.password);

            if (res.Succeeded)
            {
                var resII = await userManager.AddToRoleAsync(AppUser, "admin");

                if (resII.Succeeded)
                    return Ok(res);
            }

            return BadRequest(ModelState);
        }

    
    }
}
