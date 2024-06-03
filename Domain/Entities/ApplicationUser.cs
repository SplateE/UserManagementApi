
using Microsoft.AspNetCore.Identity; 

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    { 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }  
    }
}
