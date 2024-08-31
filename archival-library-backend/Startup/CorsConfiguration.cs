using Microsoft.Extensions.DependencyInjection;

namespace archival_library_backend.Startup;

public static class CorsConfiguration
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        return services;
    }
}
