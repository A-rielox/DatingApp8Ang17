using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);






var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // este TIENE q ir hasta ac[a arriba del pipeline

app.UseCors(x => x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();





app.MapControllers();





// para el seeding de users, va despues de MapControllers y antes de .Run
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
// try-catch p' errores durante el seeding
try
{
    var context = services.GetRequiredService<DataContext>();
    // var userManager = services.GetRequiredService<UserManager<AppUser>>();
    // var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

    await context.Database.MigrateAsync();
    // await Seed.SeedUsers(userManager, roleManager);
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}







app.Run();
