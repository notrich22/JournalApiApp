using System.Security.Claims;
using System.Text;

namespace JournalApiApp.Security
{
    public interface ISecurityUserService
    {


        // метод, проверяющий что логин/пароль пользователя валидный
        Task<bool> IsUserValidAsync(string login, string password, IPasswordEncoder encoder);



        // метод, создающий ClaimsPrincipal
        Task<ClaimsPrincipal> GetUserPrincipalAsync(string login, IPasswordEncoder encoder);
    }
}
