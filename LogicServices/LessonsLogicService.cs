using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.LogicServices
{
    public class LessonsLogicService
    {
        public async Task<List<Lesson>> GetLessons()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var lessons = await db.Lessons.Include(lesson => lesson.subject).Include(lesson => lesson.group).ToListAsync();
                    return lessons;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

    }
}
