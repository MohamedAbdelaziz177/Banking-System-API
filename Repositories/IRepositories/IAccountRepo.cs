using Banking_system.Model;

namespace Banking_system.Repositories.IRepositories
{
    public interface IAccountRepo : IGenericRepo<Account>
    {
        Task<List<Account>> GetAccountsByCustId(int custId);
    }
}
