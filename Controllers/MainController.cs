using JournalApiApp.Controllers.ApiMessages;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Mvc;
using static JournalApiApp.Messages;

namespace JournalApiApp.Controllers
{
    public class MainController
    {
        // 1. пинг
        public async Task Ping(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync(new BaseApiMessages.StringMessage("pong"));
        }
        public async Task Login(HttpContext context)
        {

            UserData user = await context.Request.ReadFromJsonAsync<UserData>();
            ISecurityUserService securityService = new DBSecurityService();
            IPasswordEncoder encoder = new SimpleEncoder();
            securityService.AddUser(user.Login, user.Password, user.Group, encoder);

        }


    }
}
