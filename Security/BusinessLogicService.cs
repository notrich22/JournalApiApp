using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Journal;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static JournalApiApp.Records;
using System.Text.RegularExpressions;

namespace JournalApiApp.Security
{
    public class BusinessLogicService
    {
        public async Task<List<Lesson>> GetLessons()
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
        public async Task<List<StudyGroup>> GetGroups()
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
        public async Task<List<Student>> GetStudents()
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
        public async Task<List<Student>> GetStudentsByGroup(int groupId)
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
        public async Task<List<Note>> GetNotesByStudent(int studentId)
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
                    Note note = await db.Notes.FirstOrDefaultAsync(u=>u.Id==noteId);
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
        public async Task<Student> AddStudent(string FullName, int StudyGroupId, int UserId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Student newStudent = new Student();
                    newStudent.FullName = FullName;
                    newStudent.StudyGroup = await db.StudyGroups.FirstOrDefaultAsync(u => u.Id == StudyGroupId);
                    newStudent.User = await db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
                    await db.Students.AddAsync(newStudent);
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
        public async Task<Student> ShowStudent(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Students.Include(u=>u.StudyGroup).Include(u=>u.User).Include(u=>u.User.UserGroup).FirstOrDefaultAsync(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Student>> ShowStudents()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Students.Include(u=>u.StudyGroup).Include(u=>u.User).Include(u=>u.User.UserGroup).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<Student> UpdateStudent(int studentId, string FullName, int StudyGroupId, int UserId)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Student student = await db.Students.FirstOrDefaultAsync(u => u.Id == studentId);
                    student.FullName = FullName;
                    student.StudyGroup = await db.StudyGroups.FirstOrDefaultAsync(group=>group.Id == StudyGroupId);
                    student.User = await db.Users.FirstOrDefaultAsync(user => user.Id == UserId);
                    await db.SaveChangesAsync();
                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    db.Students.Remove(await db.Students.FirstOrDefaultAsync(u=>u.Id == id));
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        //StudyGroup CRUD
        public async Task<StudyGroup> AddStudyGroup(string GroupName)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    StudyGroup newStudyGroup = new StudyGroup();
                    newStudyGroup.GroupName = GroupName;
                    await db.StudyGroups.AddAsync(newStudyGroup);
                    await db.SaveChangesAsync();
                    return newStudyGroup;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<StudyGroup> ShowStudyGroup(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.StudyGroups.FirstOrDefaultAsync(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<StudyGroup>> ShowStudyGroups()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.StudyGroups.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<StudyGroup> UpdateStudyGroup(int groupId, string GroupName)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    StudyGroup group = await db.StudyGroups.FirstOrDefaultAsync(u => u.Id == groupId);
                    group.GroupName = GroupName;
                    await db.SaveChangesAsync();
                    return group;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<bool> DeleteStudyGroup(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    db.StudyGroups.Remove(await db.StudyGroups.FirstOrDefaultAsync(u=>u.Id == id));
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        //Subject CRUD
        public async Task<Subject> AddSubject(string SubjectName)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Subject newSubject = new Subject();
                    newSubject.SubjectName = SubjectName;
                    await db.Subjects.AddAsync(newSubject);
                    await db.SaveChangesAsync();
                    return newSubject;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<Subject> ShowSubject(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Subjects.FirstOrDefaultAsync(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<Subject>> ShowSubjects()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Subjects.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<Subject> UpdateSubject(int subjectId, string SubjectName)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    Subject group = await db.Subjects.FirstOrDefaultAsync(u => u.Id == subjectId);
                    group.SubjectName = SubjectName;
                    await db.SaveChangesAsync();
                    return group;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<bool> DeleteSubject(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    db.Subjects.Remove(await db.Subjects.FirstOrDefaultAsync(u=>u.Id == id));
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
