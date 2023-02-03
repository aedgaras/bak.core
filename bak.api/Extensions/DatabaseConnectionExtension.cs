using bak.api.Context;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Extensions;

internal static class DatabaseConnectionExtension
{
    internal static IServiceCollection AddDatabaseContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql($"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                          $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                          $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                          $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                          $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};"));
    }
}