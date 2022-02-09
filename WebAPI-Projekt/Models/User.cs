using Microsoft.AspNetCore.Identity;

namespace WebAPI_Projekt.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
    }
}
