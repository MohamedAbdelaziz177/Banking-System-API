using Microsoft.AspNetCore.Identity;

namespace Banking_system.Model
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; } = "Egypt";

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
