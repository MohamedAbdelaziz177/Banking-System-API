using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Banking_system.Repositories.MRepositories
{
    public class AccountRepo : GenericRepo<Account>, IAccountRepo
    {
        public AccountRepo(AppDbContext con) : base(con)
        {
        }

      
        public async Task<List<Account>> GetAccountsByCustId(int custId)
        {
           
           return await dbset.Where(x => x.customerId == custId).ToListAsync();
        }

        public async Task Deposit(int accId, decimal amount)
        {
            var acc = await dbset.FindAsync(accId);
            acc.balance += amount;

            await con.SaveChangesAsync();
        }
        public async Task<bool> Withdraw(int accId, decimal amount)
        {
            var acc = await dbset.FindAsync(accId);

            if (acc.balance >= amount)
            {
                acc.balance -= amount;
                return true;
            }

            await con.SaveChangesAsync();
            return false;
        }
    }
}
