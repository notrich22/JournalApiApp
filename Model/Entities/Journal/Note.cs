using System.ComponentModel.DataAnnotations;

namespace JournalApiApp.Model.Entities.Journal
{
    public class Note
    {
        public int Id { get; set; }


        [Range(1, 5)]
        public int NoteDef { get; set; }

        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
        
    }
}
