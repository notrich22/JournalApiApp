using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.LogicServices
{
    public class SubjectsLogicService
    {
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
                    db.Subjects.Remove(await db.Subjects.FirstOrDefaultAsync(u => u.Id == id));
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
