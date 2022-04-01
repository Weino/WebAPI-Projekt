using Microsoft.AspNetCore.Identity;

namespace WebAPI_Projekt.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Password { get; internal set; }
        public string Username { get; internal set; }
    }
}
