using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace JournalApiApp.Security
{
    public class MainLogicService
    {
        public async Task<User> AddUser(string login, string password, int groupId, IPasswordEncoder encoder)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    User newUser = new User();
                    newUser.Login = encoder.Encode(login);
                    newUser.Password = encoder.Encode(password);
                    newUser.UserGroup = await db.UsersGroups.FirstOrDefaultAsync(gr=>gr.Id == groupId);
                    await db.Users.AddAsync(newUser);
                    await db.SaveChangesAsync();
                    return newUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<User>> ShowUsers()
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Users.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<User> ShowUser(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    return await db.Users.FirstOrDefaultAsync(u=>u.Id==id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<User> UpdateUser(int id, string login, string password, int groupId, IPasswordEncoder encoder)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    User newUser = await db.Users.FirstOrDefaultAsync(u=>u.Id==id);
                    newUser.Login = encoder.Encode(login);
                    newUser.Password = encoder.Encode(password);
                    newUser.UserGroup = await db.UsersGroups.FirstOrDefaultAsync(gr => gr.Id == groupId);
                    await db.SaveChangesAsync();
                    return newUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                using (var db = new JournalDbContext())
                {
                    db.Remove(await db.Users.FirstOrDefaultAsync(u => u.Id == id));
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
