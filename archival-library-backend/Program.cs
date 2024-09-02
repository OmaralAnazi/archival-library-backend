using archival_library_backend.Middlewares;
using archival_library_backend.Startup;
using DotNetEnv;

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

app.UseCors("AllowAll"); // For easier testing since it's a simple assessment project

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
