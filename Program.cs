using JournalApiApp.Controllers;
using JournalApiApp.Security;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies").AddCookie(async option =>
{
    option.LoginPath = "/login";                // обработчики установки логина
    option.AccessDeniedPath = "/access-denied"; // обработчик для запрета доступа
    option.LogoutPath = "/logout";               // обработчик для логаута
});

builder.Services.AddAuthorization();
builder.Services.AddSingleton<MainController>();
builder.Services.AddSingleton<SecurityController>();
builder.Services.AddSingleton < ISecurityUserService, DBSecurityService>();
builder.Services.AddSingleton<IPasswordEncoder, SimpleEncoder>();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", app.Services.GetRequiredService<SecurityController>().LoginGetAsync);

app.MapPost("/login", app.Services.GetRequiredService<SecurityController>().LoginPostAsync);
app.MapPost("/logout", app.Services.GetRequiredService<SecurityController>().LogoutAsync);

app.MapGet("/ping", app.Services.GetRequiredService<MainController>().Ping);
app.Map("/AccessGranted", app.Services.GetRequiredService<SecurityController>().AccessGranted);
app.Map("/admintest", app.Services.GetRequiredService<SecurityController>().AccessGrantedForAdmin);
app.Map("/access-denied", app.Services.GetRequiredService<SecurityController>().AccessDenied);

app.MapPost("/check-user", app.Services.GetRequiredService<MainController>().IsUserValid);



app.MapPost("/check-claims", app.Services.GetRequiredService<MainController>().GetUserPrincipalAsync);

app.MapPost("/AddUserJSON", app.Services.GetRequiredService<MainController>().AddUser);

app.Run();
