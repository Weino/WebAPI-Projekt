using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.Models;

namespace WebAPI_Projekt.Data
{
    public class GeoMessageDbContext : IdentityDbContext<User>
    {

        public GeoMessageDbContext(DbContextOptions<GeoMessageDbContext> options) : base(options)
        {

        }

        public DbSet<GeoMessage> GeoMessages { get; set; }
        public DbSet<User> Users { get; set; }  

        public async Task SeedDb(UserManager<User> userManager)
        {
            User TestUser = new User
            {
                UserName = "NewUserName",
                FirstName = "NewFirstName",
                LastName = "NewLastName"
            };
            await userManager.CreateAsync(TestUser, "Pw");

        }
    }
}
 