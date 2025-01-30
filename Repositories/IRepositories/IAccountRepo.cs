using Banking_system.Model;

namespace Banking_system.Repositories.IRepositories
{
    public interface IAccountRepo : IGenericRepo<Account>
    {
        Task<List<Account>> GetAccountsByCustId(int custId);

        Task Deposit(int accId, decimal amount);

        public Task<bool> Withdraw(int accId, decimal amount);


    }
}
