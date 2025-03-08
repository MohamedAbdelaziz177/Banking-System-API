using Banking_system.DTO_s.AuthDto;
using Banking_system.Model;
using Microsoft.AspNetCore.Identity;

namespace Banking_system.Services.AuthService_d
{
    public class AuthService 
    {
        /*
           private readonly UserManager<AppUser> userManager;
          private readonly IConfiguration configuration;
          private readonly IMailService mailService;

          public AuthService(UserManager<AppUser> userManager,
                             IConfiguration configuration,
                             IMailService mailService)
          {
              this.userManager = userManager;
              this.configuration = configuration;
              this.mailService = mailService;
          }

          // 1 - Register And Login
          public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
          {
              var checkExistedUserWithEmail = await userManager.FindByEmailAsync(registerDto.Email);

              if (checkExistedUserWithEmail != null)
                  return new AuthResponseDto { Message = "This email already exists" };

              AppUser appUser = new AppUser();

              appUser.Email = registerDto.Email;
              appUser.FirstName = registerDto.FirstName;
              appUser.LastName = registerDto.LastName;
              appUser.UserName = registerDto.Email;
              appUser.EmailConfirmed = false;


              var IdentRes = await userManager.CreateAsync(appUser, registerDto.Password);

              if (!IdentRes.Succeeded)
              {
                  string errors = "";

                  foreach (var error in IdentRes.Errors)
                  {
                      errors += (error.Description + ", ");
                  }

                  return new AuthResponseDto { Message = errors };
              }



              await userManager.AddToRoleAsync(appUser, "USER");

              TokenResponseDto tokens = await GenerateTokensAsync(appUser.Id);



              AuthResponseDto authDto = new AuthResponseDto
              {
                  IsAuthenticated = true,
                  Token = tokens.AccessToken,
                  RefreshToken = tokens.RefreshToken,
                  Email = registerDto.Email,
                  ExpiresOn = DateTime.Now.AddMinutes(15),
                  Roles = new List<string>() { "User" }

              };

              await SendConfirmationCode(appUser.Email);


              return authDto;

          }

          public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
          {
              var isExistedUserWithEmail = await userManager.FindByEmailAsync(loginDto.Email);

              if (isExistedUserWithEmail == null)
                  return new AuthResponseDto { Message = "Invalid Email or Password" };

              var isPasswordMatchingWithEmail = await userManager
                   .CheckPasswordAsync(isExistedUserWithEmail, loginDto.Password);

              if (!isPasswordMatchingWithEmail)
                  return new AuthResponseDto { Message = "Invalid Email or Password" };

              bool Has2FA = await userManager.GetTwoFactorEnabledAsync(isExistedUserWithEmail);

              if (Has2FA)
                  return new AuthResponseDto { Message = "You Need To Login Using 2FA", Is2FaRequired = true };




              TokenResponseDto tokens = await GenerateTokensAsync(isExistedUserWithEmail.Id);

              var roles = await userManager.GetRolesAsync(isExistedUserWithEmail);

              AuthResponseDto authDto = new AuthResponseDto
              {

                  Email = loginDto.Email,
                  IsAuthenticated = true,
                  Token = tokens.AccessToken,
                  RefreshToken = tokens.RefreshToken,
                  ExpiresOn = DateTime.Now.AddMinutes(15),
                  Roles = roles.ToList()

              };


              return authDto;

          }
         */
    }
}
