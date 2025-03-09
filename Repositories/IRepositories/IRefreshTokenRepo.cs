using Banking_system.Model;

namespace Banking_system.Repositories.IRepositories
{
    public interface IRefreshTokenRepo : IGenericRepo<RefreshToken>
    {
        Task<RefreshToken?> GetValidRefreshTokenAsync(string refreshToken, int userId);
    }
}
