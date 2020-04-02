using Microsoft.EntityFrameworkCore;
using AISPHRD.Models;

namespace AISPHRD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Conscript> Conscripts { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<MilitaryID> MilitaryIDs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId = 1,
                   Login = "Admin",
                   Password = "12345678",
               }
            );
        }
    }
}