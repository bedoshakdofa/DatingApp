using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class DbContextApplication : DbContext
    {
        public DbContextApplication(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<User> users { get; set; }  
    }
}
