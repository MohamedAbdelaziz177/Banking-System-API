using Banking_system.DAL.Data;
using Banking_system.DAL.Model;
using Banking_system.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Banking_system.DAL.Repositories.MRepositories
{
    public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(AppDbContext con) : base(con) { }

        public async Task<RefreshToken?> GetValidRefreshTokenAsync(string refreshToken)
        {
            var refTok = await dbset
                              .FirstOrDefaultAsync(x => x.Token == refreshToken
                                                                     && x.ExpiryDate > DateTime.Now
                                                                     && !x.isRevoked
                                                                     );

            return refTok;
        }
    }
}
