using Banking_system.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banking_system.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, Role, int>
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Customer>()
                .HasMany(x => x.Accounts)
                .WithOne()
                .HasForeignKey(e => e.customerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Customer>()
                   .HasMany(e => e.cards)
                   .WithOne()
                   .HasForeignKey(e => e.customerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Customer>()
                   .HasMany(e => e.loans)
                   .WithOne()
                   .HasForeignKey(e => e.customerId)
                   .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Transaction>()
                .HasOne(e => e.FromAccount)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            ;


            builder.Entity<Transaction>()
                .HasOne(e => e.ToAccount)
                .WithOne() 
                .OnDelete(DeleteBehavior.Restrict);
                


            builder.Entity<Role>()
                .HasData(SeedRoles());
 
        }

        private List<Role> SeedRoles()
        {


            var roles = new List<Role>();

            roles.Add(new Role() { Id = 1, Name = "admin" });
            roles.Add(new Role() { Id = 2, Name = "customer" });
            roles.Add(new Role() { Id = 3, Name = "user" });

            return roles;
        }




    }
}
