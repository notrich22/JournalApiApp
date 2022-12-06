using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Model.Entities.Journal;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace JournalApiApp.Model
{
    public class JournalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UsersGroup> UsersGroups { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=JournalApiAppDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UsersGroup>().ToTable("UsersGroup");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<StudyGroup>().ToTable("StudyGroup");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Note>().ToTable("Note");
        }

    }
}
