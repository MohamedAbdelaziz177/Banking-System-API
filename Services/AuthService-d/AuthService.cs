using Banking_system.Data;
using Banking_system.DTO_s.AuthDto;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Banking_system.Services.AuthService_d
{
    public class AuthService : IAuthService
    {
        
          private readonly UserManager<AppUser> userManager;
          private readonly IConfiguration configuration;
          private readonly IMailService mailService;
          private readonly IRefreshTokenRepo refreshTokenRepo;
     

        public AuthService(UserManager<AppUser> userManager,
                             IConfiguration configuration,
                             IMailService mailService,
                             IRefreshTokenRepo refreshTokenRepo)
          {
              this.userManager = userManager;
              this.configuration = configuration;
              this.mailService = mailService;
            this.refreshTokenRepo = refreshTokenRepo;
            
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


        // 4 - Tokens Generation   
       public async Task<TokenResponseDto> GenerateTokensAsync(int userId)
       {
           Console.WriteLine(userId + "haa");
       
           var accessToken = await GenerateAccessTokenAsync(userId);
           var refreshToken = await GenerateRefreshTokenAsync(userId);
       
       
           var tokenResponse = new TokenResponseDto
           {
               AccessToken = accessToken,
               RefreshToken = refreshToken.Token
           };
           return tokenResponse;
       }
       
       private async Task<string> GenerateAccessTokenAsync(int userId)
       {
           AppUser appUser = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
       
       
       
       
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
       
       
       
           SecurityKey secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
           SigningCredentials signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
       
       
           JwtSecurityToken token = new JwtSecurityToken(
               issuer: configuration["JWT:Issuer"],
               audience: configuration["JWT:Audience"],
               claims: claims,
               expires: DateTime.Now.AddHours(double.Parse(configuration["JWT:ExpiryDuration"])),
               signingCredentials: signingCredentials
           );
       
           return new JwtSecurityTokenHandler().WriteToken(token);
       
       }
       
       private async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
       {
           RefreshToken refreshToken = new RefreshToken();
       
           refreshToken.CreatedAt = DateTime.Now;
           refreshToken.isRevoked = false;
       
           refreshToken.ExpiryDate = DateTime.Now.AddDays(30);
           refreshToken.AppUser = await userManager.FindByIdAsync(userId.ToString());
       
           refreshToken.Token = Guid.NewGuid().ToString() + "_" + Guid.NewGuid().ToString();
       
           await appDbContext.RefreshTokens.AddAsync(refreshToken);
           await appDbContext.SaveChangesAsync();
           return refreshToken;
       }
       
       public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken, string userId)
        {

            var refTok = await appDbContext.RefreshTokens
                                           .Include(rt => rt.appUser)
                                           .FirstOrDefaultAsync(x => x.Token == refreshToken
                                                                     && x.ExpiryDate > DateTime.Now
                                                                     && !x.isRevoked
                                                                     && x.AppUserId == userId);

            if (refTok == null)
                return new TokenResponseDto
                {
                    Successed = false
                };

            if (refTok.ExpiryDate > DateTime.Now.AddDays(1))
            {
                var accessTok = await GenerateAccessTokenAsync(refTok.AppUserId);
                refTok.ExpiryDate = DateTime.Now.AddDays(15);

                return new TokenResponseDto
                {
                    AccessToken = accessTok,
                    RefreshToken = refTok.Token
                };
            }

            refTok.isRevoked = true;

            await appDbContext.SaveChangesAsync();

            return await GenerateTokensAsync(refTok.AppUserId);

        }


      

       

    }
}
