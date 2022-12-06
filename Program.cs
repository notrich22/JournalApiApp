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
builder.Services.AddSingleton<BusinessLogicController>();
builder.Services.AddSingleton<BusinessLogicService>();
builder.Services.AddSingleton<MainLogicService>();
builder.Services.AddSingleton < ISecurityUserService, SecurityUserService>();
builder.Services.AddSingleton<IPasswordEncoder, SimpleEncoder>();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", app.Services.GetRequiredService<SecurityController>().LoginGetAsync);
app.MapPost("/login", app.Services.GetRequiredService<SecurityController>().LoginPostAsync);
app.MapPost("/logout", app.Services.GetRequiredService<SecurityController>().LogoutAsync);

app.Map("/AccessGranted", app.Services.GetRequiredService<SecurityController>().AccessGranted);
app.Map("/admintest", app.Services.GetRequiredService<SecurityController>().AccessGrantedForAdmin);
app.Map("/access-denied", app.Services.GetRequiredService<SecurityController>().AccessDenied);

//UNAUTHORIZED
app.MapGet("/getlessons", app.Services.GetRequiredService<BusinessLogicController>().GetLessons);
app.MapGet("/getgroups", app.Services.GetRequiredService<BusinessLogicController>().GetGroups);
app.MapGet("/getstudents", app.Services.GetRequiredService<BusinessLogicController>().GetStudents);
app.MapPost("/GetStudentsByGroupAsync", app.Services.GetRequiredService<BusinessLogicController>().GetStudentsByGroupAsync);
//USER
app.MapGet("/getnotes", app.Services.GetRequiredService<BusinessLogicController>().GetAllNotes);
app.MapPost("/getnotesbystudent", app.Services.GetRequiredService<BusinessLogicController>().GetAllNotesByStudent);
app.MapPost("/GetNotesByLessonforstudent", app.Services.GetRequiredService<BusinessLogicController>().GetNotesByLessonForConcreteStudent);
app.MapPost("/GetNotesByLesson", app.Services.GetRequiredService<BusinessLogicController>().GetNotesByLesson);
//TEACHER
app.MapPost("/addnote", app.Services.GetRequiredService<BusinessLogicController>().AddNoteForStudent);
app.MapPost("/updatenote", app.Services.GetRequiredService<BusinessLogicController>().UpdateNoteForStudent);
//ADMIN
//Student CRUD
app.MapPost("/addstudent", app.Services.GetRequiredService<BusinessLogicController>().AddStudent);
app.MapPost("/showstudent", app.Services.GetRequiredService<BusinessLogicController>().ShowStudent);
app.Map("/showstudent", app.Services.GetRequiredService<BusinessLogicController>().ShowStudents);
app.MapPost("/updatestudent", app.Services.GetRequiredService<BusinessLogicController>().UpdateStudent);
app.MapPost("/deletestudent", app.Services.GetRequiredService<BusinessLogicController>().DeleteStudent);
//StudyGroup CRUD
app.MapPost("/addstudygroup", app.Services.GetRequiredService<BusinessLogicController>().AddStudyGroup);
app.MapPost("/showstudygroup", app.Services.GetRequiredService<BusinessLogicController>().ShowStudyGroup);
app.Map("/showstudygroup", app.Services.GetRequiredService<BusinessLogicController>().ShowStudyGroups);
app.MapPost("/updatestudygroup", app.Services.GetRequiredService<BusinessLogicController>().UpdateStudyGroup);
app.MapPost("/deletestudygroup", app.Services.GetRequiredService<BusinessLogicController>().DeleteStudyGroup);
//Subject CRUD
app.MapPost("/addsubject", app.Services.GetRequiredService<BusinessLogicController>().AddSubject);
app.MapPost("/showsubject", app.Services.GetRequiredService<BusinessLogicController>().ShowSubject);
app.Map("/showsubject", app.Services.GetRequiredService<BusinessLogicController>().ShowSubjects);
app.MapPost("/updatesubject", app.Services.GetRequiredService<BusinessLogicController>().UpdateSubject);
app.MapPost("/deletesubject", app.Services.GetRequiredService<BusinessLogicController>().DeleteSubject);
//User CRUD
app.MapPost("/adduser", app.Services.GetRequiredService<MainController>().AddUser);
app.Map("/showuser", app.Services.GetRequiredService<MainController>().ShowUser);
app.Map("/showusers", app.Services.GetRequiredService<MainController>().ShowUsers);
app.MapPost("/updateuser", app.Services.GetRequiredService<MainController>().UpdateUser);
app.MapPost("/deleteuser", app.Services.GetRequiredService<MainController>().DeleteUser);



//app.MapPost("/AddUserJSON", app.Services.GetRequiredService<MainController>().AddUser);

app.Run();
