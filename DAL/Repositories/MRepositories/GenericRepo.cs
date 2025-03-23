using Microsoft.EntityFrameworkCore;
using Banking_system.Model;
using Banking_system.DAL.Data;
using Banking_system.DAL.Repositories.IRepositories;

namespace Banking_system.DAL.Repositories.MRepositories

{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly AppDbContext con;
        protected readonly DbSet<T> dbset;


        public GenericRepo(AppDbContext _con)
        {
            con = _con;

            dbset = con.Set<T>();

        }

        public async Task<bool> deleteAsync(int id)
        {

            var rec = await dbset.FindAsync(id);

            if (rec == null)
            {
                return false;
            }

            dbset.Remove(rec);
            await con.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task insertAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await con.SaveChangesAsync();
        }

        public async Task updateAsync(int id, T entity)
        {
            dbset.Update(entity);
            await con.SaveChangesAsync();
        }
    }
}
