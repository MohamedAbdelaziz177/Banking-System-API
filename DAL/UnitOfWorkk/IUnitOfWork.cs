using Banking_system.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking_system.DAL.UnitOfWorkk
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepo CustomersRepo { get; }
        public ICardRepo CardsRepo { get; }
        public IAccountRepo AccountsRepo { get; }

        public ILoanRepo LoansRepo { get; }

        public ITransactionRepo TransactionsRepo { get; }

        public IRefreshTokenRepo RefreshTokensRepo { get; }



        public IDbContextTransaction BeginTransaction();





        Task<int> Complete();


    }
}
