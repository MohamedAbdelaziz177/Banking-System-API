using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.AuthDto;
using Banking_system.Model;
using Banking_system.Services.AuthService_d;
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

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthResponseDto authDto = await authService.RegisterAsync(registerDto);

            if (!authDto.IsAuthenticated)
                return BadRequest(authDto.Message);



            Response.Cookies.Append("refreshToken", authDto.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(15),
            });



            return Ok(authDto);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            AuthResponseDto authDto = await authService.LoginAsync(loginDto);

            if (!authDto.IsAuthenticated)
                return BadRequest(authDto.Message);

            Response.Cookies.Append("refreshToken", authDto.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(15),
            });

            return Ok(authDto);
        }


        [HttpGet("RefreshToken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var refToken = Request.Cookies["refreshToken"];

            Console.WriteLine(refToken);

            if (refToken == null || refToken == string.Empty) return BadRequest("No Refresh Token");


            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            TokenResponseDto tokens = await authService.RefreshTokenAsync(refToken, userId);



            if (!tokens.Successed) return BadRequest("Refresh Token Failed");


            Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(15),
            });



            return Ok(tokens);

        }

        [Authorize]
        [HttpGet("RequestConfirmation")]
        public async Task<IActionResult> RequestEmailConfirmation()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null) return BadRequest("Email is not valid");

            bool succ = await authService.SendConfirmationCode(email);

            if (!succ) return BadRequest("Try Again");

            return Ok();
        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            bool verified = await authService.ConfirmEmailAsync(confirmEmailDto.Email, confirmEmailDto.Code);

            if (!verified) return BadRequest("Email is not verified");

            return Ok("Email Verified");
        }


        /*
        
        

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
                return BadRequest("Email or password not valid");
            }

            bool found = await userManager.CheckPasswordAsync(appUser, user.password);

            if (!found) 
            {
                return BadRequest("Email or password not valid");
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
                Console.WriteLine(role);
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


        // RefreshToken

        // Reset Password

        // Forget Password

    */
    }
}
