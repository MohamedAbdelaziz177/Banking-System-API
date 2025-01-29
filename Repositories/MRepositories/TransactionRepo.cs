using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Banking_system.Repositories.MRepositories
{
    public class TransactionRepo : GenericRepo<Transaction>, ITransactionRepo
    {
        public TransactionRepo(AppDbContext con) : base(con)
        {
        }

        public async Task<List<Transaction>> GetAllTrxByAccId(int AccId)
        {
            return await dbset.Where
                (x => x.FromAccountId == AccId ||x.ToAccountId == AccId).ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTrxByFromAccId(int AccId)
        {
           
            return await dbset.Where(x => x.FromAccountId == AccId).ToListAsync();

        }

        public async Task<List<Transaction>> GetAllTrxByToAccId(int AccId)
        {
            
            return await dbset.Where(x => x.ToAccountId == AccId).ToListAsync();

        }
    }
}
