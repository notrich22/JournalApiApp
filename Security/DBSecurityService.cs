using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using System.Security.Claims;

namespace JournalApiApp.Security
{
    public class DBSecurityService : ISecurityUserService
    {
        public async Task AddUser(string login, string password, string role, IPasswordEncoder encoder)
        {
            using (var db = new JournalDbContext())
            {
                UsersGroup group = db.UsersGroups.FirstOrDefault(obj => obj.GroupName == role);
                User user = new User() { Login = login, Password = encoder.Encode(password), UserGroup = group };
                db.Users.Add(user);
                db.SaveChangesAsync();
            }
        }
        public async Task<ClaimsPrincipal> GetUserPrincipal(string login) { 
            throw new NotImplementedException();
        }
        public async Task<bool> IsUserValid(string login, string password)
        {
            throw new NotImplementedException();
        }

    }
}
