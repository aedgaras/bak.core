using bak.api.Models;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder): base(optionsBuilder)
        {

        }
    }
}
