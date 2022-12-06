using JournalApiApp.LogicServices;
using JournalApiApp.Model;
using JournalApiApp.Model.Entities.Access;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using static JournalApiApp.Controllers.ApiMessages.Records;

namespace JournalApiApp.Controllers
{
    public class MainController
    {
        /*public async Task Ping(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync(new BaseApiMessages.StringMessage("pong"));
        }
        public async Task GetUserPrincipalAsync(HttpContext context)
        {
            LoginData Login = await context.Request.ReadFromJsonAsync<LoginData>();
            ISecurityUserService securityService = new DBSecurityService();
            IPasswordEncoder passEnc = context.RequestServices.GetRequiredService<IPasswordEncoder>();
            ClaimsPrincipal claims = await securityService.GetUserPrincipalAsync(Login.login, passEnc);
            await context.Response.WriteAsJsonAsync(claims);
        }
        public async Task IsUserValid(HttpContext context)
        {
            string Login = context.Request.Form["login"];
            string Password = context.Request.Form["password"];
            ISecurityUserService securityService = new DBSecurityService();
            if (await securityService.IsUserValidAsync
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
        }*/
        private MainLogicService logicService;
        private IPasswordEncoder encoder;
        public MainController(MainLogicService logicService, IPasswordEncoder encoder)
        {
            this.logicService = logicService;
            this.encoder = encoder;
        }
        [Authorize(Roles = "admin")]
        public async Task AddUser(HttpContext context)
        {
            UserData user = await context.Request.ReadFromJsonAsync<UserData>();
            await logicService.AddUser(user.login, user.password, user.groupId, encoder);
        }
        [Authorize(Roles = "admin")]
        public async Task ShowUsers(HttpContext context)
        {
            await context.Response.WriteAsJsonAsync(await logicService.ShowUsers());
        }
        [Authorize(Roles = "admin")]
        public async Task ShowUser(HttpContext context)
        {
            IdData id = await context.Request.ReadFromJsonAsync<IdData>();

            await context.Response.WriteAsJsonAsync(logicService.ShowUser(id.id));
        }
        [Authorize(Roles = "admin")]
        public async Task UpdateUser(HttpContext context)
        {
            UpdateUserData user = await context.Request.ReadFromJsonAsync<UpdateUserData>();
            await context.Response.WriteAsJsonAsync(logicService.UpdateUser(user.id, user.login, user.password, user.groupId, encoder));
        }
        [Authorize(Roles = "admin")]
        public async Task DeleteUser(HttpContext context)
        {
            IdData id = await context.Request.ReadFromJsonAsync<IdData>();
            await context.Response.WriteAsJsonAsync(logicService.DeleteUser(id.id));
        }
    }
}
