using JournalApiApp.Controllers.ApiMessages;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using static JournalApiApp.Messages;

namespace JournalApiApp.Controllers
{
    public class MainController
    {
        // 1. пинг
        [Authorize]
        public async Task Ping(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync(new BaseApiMessages.StringMessage("pong"));
        }
        public async Task AddUser(HttpContext context)
        {

            UserData user = await context.Request.ReadFromJsonAsync<UserData>();
            ISecurityUserService securityService = new DBSecurityService();
            await securityService.AddUserAsync(user.Login, user.Password, user.Group, context.RequestServices.GetRequiredService<IPasswordEncoder>());

        }
        public async Task<ClaimsPrincipal> GetUserPrincipalAsync(HttpContext context)
        {
            string Login = context.Request.Form["login"];
            ISecurityUserService securityService = new DBSecurityService();
            return await securityService.GetUserPrincipalAsync(Login,
                context.RequestServices.GetRequiredService<IPasswordEncoder>()
            );
        }
        public async Task IsUserValid(HttpContext context)
        {
            string Login = context.Request.Form["login"];
            string Password = context.Request.Form["password"];
            ISecurityUserService securityService = new DBSecurityService();
            if(await securityService.IsUserValidAsync
                (Login,
                Password,
                context.RequestServices.GetRequiredService<IPasswordEncoder>()
                ))
            {
                context.RequestServices.GetService<ILogger<Program>>().LogInformation("User is valid!");
            }
            else
            {
                context.RequestServices.GetService<ILogger<Program>>().LogInformation("User is not valid!");
            }
        }

    }
}
