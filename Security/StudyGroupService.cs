using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Journal;
using System.Security.Claims;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace JournalApiApp.Security
{
    public class StudyGroupService
    {
        public async Task<List<StudyGroup>> GetStudyGroupsAsync()
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
                return null;
            }
        }
        public async Task<StudyGroup> GetStudyGroupByIdAsync(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.StudyGroups.FirstOrDefaultAsync(obj => obj.Id == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task AddStudyGroup(StudyGroup sg)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    await db.StudyGroups.AddAsync(sg);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
