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
builder.Services.AddSingleton<StudyGroupController>();
builder.Services.AddSingleton<StudyGroupService>();
builder.Services.AddSingleton<BusinessLogicController>();
builder.Services.AddSingleton<BusinessLogicService>();
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


//UNAUTHORIZED
app.MapGet("/getlessons", app.Services.GetRequiredService<BusinessLogicController>().GetLessons);
app.MapGet("/getgroups", app.Services.GetRequiredService<BusinessLogicController>().GetGroups);
app.MapGet("/getstudents", app.Services.GetRequiredService<BusinessLogicController>().GetStudents);
app.MapPost("/GetStudentsByGroupAsync", app.Services.GetRequiredService<BusinessLogicController>().GetStudentsByGroupAsync);
//USER
app.MapGet("/getnotes", app.Services.GetRequiredService<BusinessLogicController>().GetAllNotes);
app.MapGet("/getnotesbystudent", app.Services.GetRequiredService<BusinessLogicController>().GetAllNotesByStudent);
app.MapGet("/GetNotesByLessonforstudent", app.Services.GetRequiredService<BusinessLogicController>().GetNotesByLessonForConcreteStudent);
app.MapGet("/GetNotesByLesson", app.Services.GetRequiredService<BusinessLogicController>().GetNotesByLesson);
//TEACHER
app.MapPost("/addnote", app.Services.GetRequiredService<BusinessLogicController>().AddNoteForStudent);
app.MapPost("/updatenote", app.Services.GetRequiredService<BusinessLogicController>().UpdateNoteForStudent);
//ADMIN
app.MapPost("/addstudent", app.Services.GetRequiredService<BusinessLogicController>().AddStudent);


app.MapPost("/check-claims", app.Services.GetRequiredService<MainController>().GetUserPrincipalAsync);

app.MapPost("/AddUserJSON", app.Services.GetRequiredService<MainController>().AddUser);

app.Run();
