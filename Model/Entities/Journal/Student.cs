using JournalApiApp.Model.Entities.Access;

namespace JournalApiApp.Model.Entities.Journal
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set;}
        public StudyGroup? StudyGroup { get; set; }
        public User User { get; set; } 
    }
}
