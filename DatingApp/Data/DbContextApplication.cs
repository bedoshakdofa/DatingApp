using DatingApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class DbContextApplication : DbContext
    {
        public DbContextApplication(DbContextOptions options):base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> users { get; set; }  

        public DbSet<UserLikes> likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLikes>()
                .HasKey(k => new { k.SourceUserId, k.TragetUserId });

            modelBuilder.Entity<UserLikes>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(k => k.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLikes>()
                .HasOne(s=>s.TragerUser)
                .WithMany(l=>l.LikedByUsers)
                .HasForeignKey(k=>k.TragetUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
