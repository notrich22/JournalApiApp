using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Journal;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static JournalApiApp.Records;

namespace JournalApiApp.Security
{
    public class BusinessLogicService
    {
        public async Task<List<Lesson>> GetLessonsAsync()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var lessons = await db.Lessons.Include(lesson=>lesson.subject).Include(lesson=>lesson.group).ToListAsync();
                    return lessons;
                }
            }catch(Exception ex) { 
                Console.WriteLine(ex.ToString()); 
                return null;
            }
        }
        public async Task<List<StudyGroup>> GetGroupsAsync()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var groups = await db.StudyGroups.ToListAsync();
                    return groups;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var students = await db.Students.Include(st=>st.StudyGroup).ToListAsync();
                    return students;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Student>> GetStudentsByGroupAsync(int groupId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    List<Student> students = new List<Student>();
                    foreach(var student in db.Students.Include(u=>u.StudyGroup))
                    {
                        if(student.StudyGroup.Id == groupId)
                        {
                            students.Add(student);
                        }
                    }
                    return students;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Note>> GetNotesAsync()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var notes = await db.Notes
                        .Include(u => u.Lesson)
                        .Include(u => u.Lesson.group)
                        .Include(u => u.Lesson.subject)
                        .Include(u=>u.Student)
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
        public async Task<List<Note>> GetNotesByStudentAsync(int studentId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    List<Note> notes = new List<Note>();
                    foreach (var note in db.Notes
                        .Include(u=>u.Student)
                        .Include(u=>u.Lesson)
                        .Include(u=>u.Lesson.group)
                        .Include(u=>u.Lesson.subject)
                        .Include(u=>u.Student.StudyGroup)
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
                using(var db = new JournalDbContext())
                {
                    Note note = new Note();
                    note.Student = await db.Students.FirstOrDefaultAsync(u => u.Id == studentId);
                    note.NoteDef = noteDef;
                    note.Lesson = await db.Lessons.FirstOrDefaultAsync(u => u.Id == lessonId);
                    await db.Notes.AddAsync(note);
                    await db.SaveChangesAsync();
                }
            }catch(Exception ex)
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
                    Note note = db.Notes.FirstOrDefault(u=>u.Id==noteId);
                    note.NoteDef = noteDef;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //admin
        //STUDENT CRUD
        public async Task<Student> AddStudent(string FullName, int GroupId, int UserId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Student newStudent = new Student();
                    newStudent.FullName = FullName;
                    newStudent.StudyGroup = await db.StudyGroups.FirstOrDefaultAsync(u => u.Id == GroupId);
                    newStudent.User = await db.Users.Include(u=>u.UserGroup).FirstOrDefaultAsync(u => u.Id == UserId);
                    db.Students.AddAsync(newStudent);
                    await db.SaveChangesAsync();
                    return newStudent;
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
