using System.ComponentModel.DataAnnotations;

namespace JournalApiApp.Model.Entities.Journal
{
    public class Lesson
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public StudyGroup group { get; set; }
        public Subject subject { get; set; }
    }
}
