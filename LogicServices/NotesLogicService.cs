using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.LogicServices
{
    public class NotesLogicService
    {
        public async Task<List<Note>> GetNotes()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var notes = await db.Notes
                        .Include(u => u.Lesson)
                        .Include(u => u.Lesson.group)
                        .Include(u => u.Lesson.subject)
                        .Include(u => u.Student)
                        .ToListAsync();
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Note>> GetNotesByStudent(int studentId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    List<Note> notes = new List<Note>();
                    foreach (var note in db.Notes
                        .Include(u => u.Student)
                        .Include(u => u.Lesson)
                        .Include(u => u.Lesson.group)
                        .Include(u => u.Lesson.subject)
                        .Include(u => u.Student.StudyGroup)
                        )
                    {
                        if (note.Student.Id == studentId)
                        {
                            notes.Add(note);
                        }
                    }
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Note>> GetNotesByLesson(int lessonId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    List<Note> notes = new List<Note>();
                    foreach (var note in db.Notes
                        .Include(u => u.Student)
                        .Include(u => u.Lesson)
                        .Include(u => u.Lesson.group)
                        .Include(u => u.Lesson.subject)
                        .Include(u => u.Student.StudyGroup)
                        )
                    {
                        if (note.Lesson.Id == lessonId)
                        {
                            notes.Add(note);
                        }
                    }
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Note>> GetNotesByLessonForConcreteStudent(int lessonId, int studentId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    List<Note> notes = new List<Note>();
                    foreach (var note in db.Notes
                        .Include(u => u.Student)
                        .Include(u => u.Lesson)
                        .Include(u => u.Lesson.group)
                        .Include(u => u.Lesson.subject)
                        .Include(u => u.Student.StudyGroup)
                        )
                    {
                        if (note.Lesson.Id == lessonId && note.Student.Id == studentId)
                        {
                            notes.Add(note);
                        }
                    }
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task AddNote(int noteDef, int studentId, int lessonId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Note note = new Note();
                    note.Student = await db.Students.FirstOrDefaultAsync(u => u.Id == studentId);
                    note.NoteDef = noteDef;
                    note.Lesson = await db.Lessons.FirstOrDefaultAsync(u => u.Id == lessonId);
                    await db.Notes.AddAsync(note);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task UpdateNote(int noteDef, int noteId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Note note = await db.Notes.FirstOrDefaultAsync(u => u.Id == noteId);
                    note.NoteDef = noteDef;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
