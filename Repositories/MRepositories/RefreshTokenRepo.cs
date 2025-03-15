using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Banking_system.Repositories.MRepositories
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
