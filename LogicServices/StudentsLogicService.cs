using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.LogicServices
{
    public class StudentsLogicService
    {
        public async Task<List<Student>> GetStudents()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    var students = await db.Students.Include(st => st.StudyGroup).ToListAsync();
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
                    foreach (var student in db.Students.Include(u => u.StudyGroup))
                    {
                        if (student.StudyGroup.Id == groupId)
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
                    return await db.Students.Include(u => u.StudyGroup).Include(u => u.User).Include(u => u.User.UserGroup).FirstOrDefaultAsync(u => u.Id == id);
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
                    return await db.Students.Include(u => u.StudyGroup).Include(u => u.User).Include(u => u.User.UserGroup).ToListAsync();
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
                    student.StudyGroup = await db.StudyGroups.FirstOrDefaultAsync(group => group.Id == StudyGroupId);
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
                    db.Students.Remove(await db.Students.FirstOrDefaultAsync(u => u.Id == id));
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
