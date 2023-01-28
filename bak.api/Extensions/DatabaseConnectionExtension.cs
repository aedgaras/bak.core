using bak.api.Context;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Extensions;

internal static class DatabaseConnectionExtension
{
    internal static IServiceCollection AddDatabaseContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Server");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

        return services;
    }
}