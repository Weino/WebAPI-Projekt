using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.Models;

namespace WebAPI_Projekt.Data
{
    public class GeoMessageDbContext : DbContext
    {

        public GeoMessageDbContext(DbContextOptions<GeoMessageDbContext> options) : base(options)
        {

        }

        public DbSet<GeoMessage> GeoMessages { get; set; }

    }

}
 