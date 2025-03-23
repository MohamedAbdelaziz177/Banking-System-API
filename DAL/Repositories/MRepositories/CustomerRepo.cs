using Banking_system.DAL.Data;
using Banking_system.DAL.Model;
using Banking_system.DAL.Repositories.IRepositories;

namespace Banking_system.DAL.Repositories.MRepositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(AppDbContext _con) : base(_con)
        {
        }


    }
}
