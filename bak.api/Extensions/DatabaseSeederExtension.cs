using bak.api.Context;

namespace bak.api.Extensions;

internal static class DatabaseSeederExtension
{
    internal static IApplicationBuilder UseDatabaseSeed(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            DatabaseSeeder.SeedDatabase(context);
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}