using JournalApiApp.Controllers.ApiMessages;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
        public async Task AddUser(HttpContext context)
        {

            UserData user = await context.Request.ReadFromJsonAsync<UserData>();
            ISecurityUserService securityService = new DBSecurityService();
            await securityService.AddUser(user.Login, user.Password, user.Group, context.RequestServices.GetRequiredService<IPasswordEncoder>());

        }
        public async Task IsUserValid(HttpContext context)
        {
            var logger = context.RequestServices.GetService<ILogger<Program>>();
            string Login = context.Request.Form["login"];
            IPasswordEncoder encoder = new SimpleEncoder();
            string Password = context.Request.Form["password"];
            ISecurityUserService securityService = new DBSecurityService();
            if(await securityService.IsUserValid
                (Login,
                Password,
                context.RequestServices.GetRequiredService<IPasswordEncoder>(),
                context.RequestServices.GetService<ILogger<Program>>()
                ))
            {
                logger.LogInformation("User is valid!");
            }
            else
            {
                logger.LogInformation("User is not valid!");
            }
        }

    }
}
