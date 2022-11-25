using JournalApiApp.Controllers;
using JournalApiApp.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
// ���������� ������������
builder.Services.AddSingleton<MainController>();
builder.Services.AddSingleton<IPasswordEncoder>(new SimpleEncoder());
var app = builder.Build();


app.MapGet("/ping", app.Services.GetRequiredService<MainController>().Ping);
app.MapPost("/AddUser", app.Services.GetRequiredService<MainController>().AddUser);
app.MapPost("/check-user", app.Services.GetRequiredService<MainController>().IsUserValid);
app.Map("/access-denied", async (context) => { });

app.Run();
