using JournalApiApp.Model.Entities.Access;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.Model
{
    public class JournalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UsersGroup> UsersGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=JournalApiAppDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UsersGroup>().ToTable("UsersGroup");
        }

    }
}
