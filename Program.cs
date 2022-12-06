using JournalApiApp.Controllers;
using JournalApiApp.Controllers.BusinessLogicControllers;
using JournalApiApp.LogicServices;
using JournalApiApp.Security;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies").AddCookie(async option =>
{
    option.LoginPath = "/login";                // обработчики установки логина
    option.AccessDeniedPath = "/access-denied"; // обработчик для запрета доступа
    option.LogoutPath = "/logout";               // обработчик для логаута
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton<LessonsController>();
builder.Services.AddSingleton<NotesController>();
builder.Services.AddSingleton<StudentsController>();
builder.Services.AddSingleton<StudyGroupsController>();
builder.Services.AddSingleton<SubjectsController>();

builder.Services.AddSingleton<LessonsLogicService>();
builder.Services.AddSingleton<NotesLogicService>();
builder.Services.AddSingleton<StudentsLogicService>();
builder.Services.AddSingleton<StudyGroupsLogicService>();
builder.Services.AddSingleton<SubjectsLogicService>();


builder.Services.AddSingleton<MainController>();
builder.Services.AddSingleton<MainLogicService>();
builder.Services.AddSingleton<SecurityController>();

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
app.MapGet("/getlessons", app.Services.GetRequiredService<LessonsController>().GetLessons);
app.MapGet("/getgroups", app.Services.GetRequiredService<StudyGroupsController>().GetGroups);
app.MapGet("/getstudents", app.Services.GetRequiredService<StudentsController>().GetStudents);
app.MapPost("/GetStudentsByGroup", app.Services.GetRequiredService<StudentsController>().GetStudentsByGroupAsync);
//USER
app.MapGet("/getnotes", app.Services.GetRequiredService<NotesController>().GetAllNotes);
app.MapPost("/getnotesbystudent", app.Services.GetRequiredService<NotesController>().GetAllNotesByStudent);
app.MapPost("/GetNotesByLessonforstudent", app.Services.GetRequiredService<NotesController>().GetNotesByLessonForConcreteStudent);
app.MapPost("/GetNotesByLesson", app.Services.GetRequiredService<NotesController>().GetNotesByLesson);
//TEACHER
app.MapPost("/addnote", app.Services.GetRequiredService<NotesController>().AddNoteForStudent);
app.MapPost("/updatenote", app.Services.GetRequiredService<NotesController>().UpdateNoteForStudent);
//ADMIN
//Student CRUD
app.MapPost("/addstudent", app.Services.GetRequiredService<StudentsController>().AddStudent);
app.MapPost("/showstudent", app.Services.GetRequiredService<StudentsController>().ShowStudent);
app.Map("/showstudent", app.Services.GetRequiredService<StudentsController>().ShowStudents);
app.MapPost("/updatestudent", app.Services.GetRequiredService<StudentsController>().UpdateStudent);
app.MapPost("/deletestudent", app.Services.GetRequiredService<StudentsController>().DeleteStudent);
//StudyGroup CRUD
app.MapPost("/addstudygroup", app.Services.GetRequiredService<StudyGroupsController>().AddStudyGroup);
app.MapPost("/showstudygroup", app.Services.GetRequiredService<StudyGroupsController>().ShowStudyGroup);
app.Map("/showstudygroup", app.Services.GetRequiredService<StudyGroupsController>().ShowStudyGroups);
app.MapPost("/updatestudygroup", app.Services.GetRequiredService<StudyGroupsController>().UpdateStudyGroup);
app.MapPost("/deletestudygroup", app.Services.GetRequiredService<StudyGroupsController>().DeleteStudyGroup);
//Subject CRUD
app.MapPost("/addsubject", app.Services.GetRequiredService<SubjectsController>().AddSubject);
app.MapPost("/showsubject", app.Services.GetRequiredService<SubjectsController>().ShowSubject);
app.Map("/showsubject", app.Services.GetRequiredService<SubjectsController>().ShowSubjects);
app.MapPost("/updatesubject", app.Services.GetRequiredService<SubjectsController>().UpdateSubject);
app.MapPost("/deletesubject", app.Services.GetRequiredService<SubjectsController>().DeleteSubject);
//User CRUD
app.MapPost("/adduser", app.Services.GetRequiredService<MainController>().AddUser);
app.Map("/showuser", app.Services.GetRequiredService<MainController>().ShowUser);
app.Map("/showusers", app.Services.GetRequiredService<MainController>().ShowUsers);
app.MapPost("/updateuser", app.Services.GetRequiredService<MainController>().UpdateUser);
app.MapPost("/deleteuser", app.Services.GetRequiredService<MainController>().DeleteUser);


app.Run();
