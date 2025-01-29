using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;
using Banking_system.Repositories.MRepositories;

namespace Banking_system.UnitOfWork
{
    public class UnitOfWorkk : IUnitOfWork
    {
        private readonly AppDbContext con;

        public ICustomerRepo CustomersRepo { get; private set; }
        public ICardRepo CardsRepo { get; private set; }
        public IAccountRepo AccountsRepo { get; private set; }
        public ILoanRepo LoansRepo { get; private set; }
        public ITransactionRepo TransactionsRepo { get; private set; }

        public UnitOfWorkk(AppDbContext con)
        {
            this.con = con;

            CustomersRepo = new CustomerRepo(con);
            CardsRepo = new CardRepo(con);//new GenericRepo<Card>(con);
            AccountsRepo = new AccountRepo(con);
            LoansRepo = new LoanRepo(con);
            TransactionsRepo = new TransactionRepo(con);

        }
       
        public async Task<int> Complete()
        {
          
            return await con.SaveChangesAsync();
        }

       
        void IDisposable.Dispose()
        {
            con.Dispose();
        }
    }
}
