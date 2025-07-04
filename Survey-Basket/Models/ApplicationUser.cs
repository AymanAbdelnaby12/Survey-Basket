using Microsoft.AspNetCore.Identity;

namespace Survey_Basket.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
    }
}
