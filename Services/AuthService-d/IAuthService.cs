using Banking_system.DTO_s;
using Banking_system.DTO_s.AuthDto;

namespace Banking_system.Services.AuthService_d
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

        Task<TokenResponseDto> RefreshTokenAsync(string refreshToken, string userId);

        Task<bool> ConfirmEmailAsync(string email, string code);

        Task<bool> SendConfirmationCode(string email);

        Task<bool> ForgetPasswordAsync(string email);

        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
