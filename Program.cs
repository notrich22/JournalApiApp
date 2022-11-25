using JournalApiApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// ���������� ������������
builder.Services.AddSingleton<MainController>();

var app = builder.Build();


app.MapGet("/ping", app.Services.GetRequiredService<MainController>().Ping);
app.MapPost("/login", app.Services.GetRequiredService<MainController>().Login);
app.Map("/logout", async (context) => { });
app.Map("/access-denied", async (context) => { });

app.Run();
