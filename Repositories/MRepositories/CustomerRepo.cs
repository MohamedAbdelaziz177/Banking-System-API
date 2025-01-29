using Banking_system.Data;
using Banking_system.Model;
using Banking_system.Repositories.IRepositories;

namespace Banking_system.Repositories.MRepositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(AppDbContext _con) : base(_con)
        {
        }

     
    }
}
