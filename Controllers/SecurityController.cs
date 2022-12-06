using JournalApiApp.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using System.Security.Claims;
using static JournalApiApp.Records;

namespace JournalApiApp.Controllers
{
    public class SecurityController
    {
        private ISecurityUserService securityUserService;
        private IPasswordEncoder encoder;
        public SecurityController(ISecurityUserService securityUserService,
            IPasswordEncoder encoder)
        {
            this.securityUserService = securityUserService;
            this.encoder = encoder;
        }
        public async Task LoginGetAsync(HttpContext context)
        {
            await context.Response.WriteAsJsonAsync(new StringMessage("Sign in to see this info"));
        }
        public async Task LoginPostAsync(HttpContext context)
        {
            try { 
                UserLogin user = await context.Request.ReadFromJsonAsync<UserLogin>();
                if (await securityUserService.IsUserValidAsync(user.Login, user.Password, encoder)) {
                    ClaimsPrincipal ClaimsPrinc = await securityUserService.GetUserPrincipalAsync(user.Login, encoder);
                    await context.SignInAsync("Cookies", ClaimsPrinc);
                    context.Response.Redirect("/");
                }
                else
                {
                    context.Response.Redirect("/access-denied");
                }
                
            }catch(Exception ex) {
                Console.Write(ex.ToString());
                context.Response.Redirect("/access-denied");
            }
        }
        public async Task AccessDenied(HttpContext context) 
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new StringMessage("Access denied"));
        }
        [Authorize (Roles = "user || admin")]
        public async Task AccessGranted(HttpContext context) 
        {
            await context.Response.WriteAsJsonAsync(new StringMessage("Access granted for everyone"));
        }
        [Authorize(Roles = "admin")]
        public async Task AccessGrantedForAdmin(HttpContext context) 
        {
            await context.Response.WriteAsJsonAsync(new StringMessage("Access granted only for admin"));
        }
        public async Task LogoutAsync(HttpContext context)
        {
            try
            {
                await context.SignOutAsync();
                await context.Response.WriteAsJsonAsync(new StringMessage("You were successfully logged out"));
                context.Response.Redirect("/ping");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                //logger.LogError(ex.Message);
                return;
            }
        }

    }
}
