using archival_library_backend.Data;
using archival_library_backend.Middlewares;
using archival_library_backend.Startup;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

if (environment == "Development")
    Env.TraversePath().Load(".env.development");
else if (environment == "Production")
    Env.TraversePath().Load(".env.production");

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();  // Environment variables overwrite previous settings

builder.Services.AddCustomCors()
                .AddCustomSwagger()
                .AddCustomDbContext(builder.Configuration)
                .AddCustomIdentity()
                .AddCustomAuthentication(builder.Configuration)
                .AddCustomServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Apply any pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseCors("AllowAll"); // For easier testing since it's a simple assessment project

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
