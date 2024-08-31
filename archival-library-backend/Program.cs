using archival_library_backend.Middlewares;
using archival_library_backend.Startup;

var builder = WebApplication.CreateBuilder(args);

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
