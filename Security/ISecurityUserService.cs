using System.Security.Claims;
using System.Text;

namespace JournalApiApp.Security
{
    public interface ISecurityUserService
    {
        Task AddUser(string login, string password, string role, IPasswordEncoder encoder);



        // метод, проверяющий что логин/пароль пользователя валидный
        Task<bool> IsUserValid(string login, string password, IPasswordEncoder encoder, ILogger logger);



        // метод, создающий ClaimsPrincipal
        Task<ClaimsPrincipal> GetUserPrincipal(string login);
    }
}
