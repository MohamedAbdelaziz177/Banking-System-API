using Banking_system.Model;

namespace Banking_system.Repositories.IRepositories
{
    public interface ICardRepo : IGenericRepo<Card>
    {
        Task<List<Card>> GetCardsByCustId(int custId);

    }
}
