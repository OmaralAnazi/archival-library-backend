using archival_library_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace archival_library_backend.Startup;

public static class DbContextConfiguration
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
