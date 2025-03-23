using Banking_system.DAL.Data;
using Banking_system.DAL.Repositories.IRepositories;
using Banking_system.Model;
using Banking_system.Repositories.MRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking_system.DAL.UnitOfWorkk
{
    public class UnitOfWorkk : IUnitOfWork
    {
        private readonly AppDbContext con;

        public ICustomerRepo CustomersRepo { get; private set; }
        public ICardRepo CardsRepo { get; private set; }
        public IAccountRepo AccountsRepo { get; private set; }
        public ILoanRepo LoansRepo { get; private set; }
        public ITransactionRepo TransactionsRepo { get; private set; }

        public IRefreshTokenRepo RefreshTokensRepo { get; private set; }

        public UnitOfWorkk(AppDbContext con)
        {
            this.con = con;

            CustomersRepo = new CustomerRepo(con);
            CardsRepo = new CardRepo(con);  //new GenericRepo<Card>(con);
            AccountsRepo = new AccountRepo(con);
            LoansRepo = new LoanRepo(con);
            TransactionsRepo = new TransactionRepo(con);
            RefreshTokensRepo = new RefreshTokenRepo(con);

        }

        public async Task<int> Complete()
        {

            return await con.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return con.Database.BeginTransaction();
        }



        void IDisposable.Dispose()
        {
            con.Dispose();
        }
    }
}
