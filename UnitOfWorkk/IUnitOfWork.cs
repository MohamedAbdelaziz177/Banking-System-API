using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking_system.UnitOfWork
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
