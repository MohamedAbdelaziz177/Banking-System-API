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


        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string email)
        {
            bool succ = await authService.ForgetPasswordAsync(email);

            if (!succ) return BadRequest("Something Went Wrong");

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool succ = await authService.ResetPasswordAsync(resetPasswordDto);

            if (!succ) return BadRequest("Password not reset, Something went wrong");

            return Ok("Password Reset Successfully");
        }


       /*
    
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

         */

    }
}
