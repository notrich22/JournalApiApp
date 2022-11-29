using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace JournalApiApp.Security
{
    public class DBSecurityService : ISecurityUserService
    {
        public async Task AddUserAsync(string login, string password, string role, IPasswordEncoder encoder)
        {
            using (var db = new JournalDbContext())
            {
                UsersGroup group = await db.UsersGroups.FirstOrDefaultAsync(obj => obj.GroupName == role);
                User user = new User() { 
                    Login = encoder.Encode(login),
                    Password = encoder.Encode(login + password),
                    UserGroup = group
                };
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }
        }
        public async Task<ClaimsPrincipal> GetUserPrincipalAsync(string login, IPasswordEncoder encoder) {
            try { 
                using (var db = new JournalDbContext())
                {
                    var user = await db.Users.FirstOrDefaultAsync(user => user.Login == encoder.Encode(login));
                    UsersGroup group = await db.UsersGroups.FirstOrDefaultAsync(obj => obj.Id == user.UserGroupId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role, group.GroupName)
                    };
                    return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));  
                }
            }catch(Exception ex) {
                return null;
            }
        }
        public async Task<bool> IsUserValidAsync(string login, string password, IPasswordEncoder encoder)
        {
            using (var db = new JournalDbContext())
            {
                try { 
                    User user = await db.Users
                            .FirstAsync(obj => obj.Login == encoder.Encode(login));
                    if(user == null) { 
                        return false; 
                    }
                    if (user.Login == encoder.Encode(login) &&
                            user.Password == encoder.Encode(login + password))
                    {
                        return true;
                    }
                    else return false;
                }catch(Exception ex)
                {
                    return false;
                }
            }
        }

    }
}
