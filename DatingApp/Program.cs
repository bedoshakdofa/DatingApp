using DatingApp.Extentions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
