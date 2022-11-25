using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace JournalApiApp.Security
{
    public class DBSecurityService : ISecurityUserService
    {
        public async Task AddUser(string login, string password, string role, IPasswordEncoder encoder)
        {
            using (var db = new JournalDbContext())
            {
                UsersGroup group = await db.UsersGroups.FirstAsync(obj => obj.GroupName == role);
                User user = new User() { Login = encoder.Encode(login), Password = encoder.Encode(password), UserGroup = group };
                db.Users.Add(user);
                db.SaveChangesAsync();
            }
        }
        public async Task<ClaimsPrincipal> GetUserPrincipal(string login) { 
            throw new NotImplementedException();
        }
        public async Task<bool> IsUserValid(string login, string password, IPasswordEncoder encoder, ILogger logger)
        {
            using (var db = new JournalDbContext())
            {
                try { 
                User user = await db.Users.FirstAsync(obj => obj.Login == encoder.Encode(login));
                if(user == null) { 
                    return false; 
                }
                if (user.Login == encoder.Encode(login) && user.Password == encoder.Encode(password))
                {
                    return true;
                }
                else return false;
                }catch(Exception ex)
                {
                    logger.LogError(ex.Message);
                    return false;
                }
            }
        }

    }
}
