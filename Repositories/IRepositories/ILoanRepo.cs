using Banking_system.Model;

namespace Banking_system.Repositories.IRepositories
{
    public interface ILoanRepo : IGenericRepo<Loan>
    {
        Task<List<Loan>> GetLoansByCustId(int custId);

    }
}
