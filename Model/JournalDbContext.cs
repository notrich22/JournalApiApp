using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Model.Entities.Journal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            // получаем файл конфигурации
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            // устанавливаем для контекста строку подключения
            string connectionStringKey = configuration.GetSection("UseConnection").Value;
            option.UseNpgsql(configuration.GetConnectionString(connectionStringKey), options => options.SetPostgresVersion(new Version(9, 6)));
        }
        

    }
}
