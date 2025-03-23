using Banking_system.DAL.Model;

namespace Banking_system.DAL.Repositories.IRepositories
{
    public interface ILoanRepo : IGenericRepo<Loan>
    {
        Task<List<Loan>> GetLoansByCustId(int custId);

    }
}
