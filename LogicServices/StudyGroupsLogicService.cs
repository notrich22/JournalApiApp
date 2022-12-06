using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.LogicServices
{
    public class StudyGroupsLogicService
    {
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
                    db.StudyGroups.Remove(await db.StudyGroups.FirstOrDefaultAsync(u => u.Id == id));
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
