using Banking_system.DAL.Data;
using Banking_system.DAL.Model;
using Banking_system.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Banking_system.DAL.Repositories.MRepositories
{
    public class LoanRepo : GenericRepo<Loan>, ILoanRepo
    {
        public LoanRepo(AppDbContext con) : base(con)
        {
        }

        public async Task<List<Loan>> GetLoansByCustId(int custId)
        {
            return await dbset.Where(x => x.customerId == custId).ToListAsync();
        }
    }
}
