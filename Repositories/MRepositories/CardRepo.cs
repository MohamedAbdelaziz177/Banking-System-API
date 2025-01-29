using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Banking_system.Repositories.MRepositories
{
    public class CardRepo : GenericRepo<Card>, ICardRepo
    {
        public CardRepo(AppDbContext con) : base(con)
        {
        }

        public async Task<List<Card>> GetCardsByCustId(int custId)
        {
            return await dbset.Where(x => x.customerId == custId).ToListAsync();

        }
    }
}
