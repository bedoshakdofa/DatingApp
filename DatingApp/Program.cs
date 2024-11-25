using DatingApp.Data;
using DatingApp.Extentions;
using DatingApp.Middleware;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

var scope=app.Services.CreateScope();

try
{
    var context=scope.ServiceProvider.GetRequiredService<DbContextApplication>();
    await context.Database.MigrateAsync();
    await Seed.GetSeed(context);
}catch(Exception ex)
{
    var logger=scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"there is problem is seeding data");
}

app.Run();
