using Banking_system.DAL.Model;

namespace Banking_system.DAL.Repositories.IRepositories
{
    public interface IAccountRepo : IGenericRepo<Account>
    {
        Task<List<Account>> GetAccountsByCustId(int custId);

        Task Deposit(int accId, decimal amount);

        public Task<bool> Withdraw(int accId, decimal amount);


    }
}
