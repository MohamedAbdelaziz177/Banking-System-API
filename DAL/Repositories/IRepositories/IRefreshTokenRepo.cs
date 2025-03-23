using Banking_system.DAL.Model;

namespace Banking_system.DAL.Repositories.IRepositories
{
    public interface IRefreshTokenRepo : IGenericRepo<RefreshToken>
    {
        Task<RefreshToken?> GetValidRefreshTokenAsync(string refreshToken);
    }
}
