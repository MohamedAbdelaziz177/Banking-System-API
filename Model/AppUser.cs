using Microsoft.AspNetCore.Identity;

namespace Banking_system.Model
{
    public class AppUser : IdentityUser<int>
    {
        public string Address { get; set; }

    }
}
