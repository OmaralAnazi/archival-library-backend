using archival_library_backend.Interfaces;
using archival_library_backend.Repositories;
using archival_library_backend.Services;

namespace archival_library_backend.Startup;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
