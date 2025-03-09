using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;

namespace Banking_system.Repositories.MRepositories
{
    public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(AppDbContext con) : base(con) { }
    }
}
