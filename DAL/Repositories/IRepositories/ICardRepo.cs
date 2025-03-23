using Banking_system.DAL.Model;

namespace Banking_system.DAL.Repositories.IRepositories
{
    public interface ICardRepo : IGenericRepo<Card>
    {
        Task<List<Card>> GetCardsByCustId(int custId);

    }
}
