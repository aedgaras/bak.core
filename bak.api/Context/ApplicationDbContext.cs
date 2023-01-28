using bak.api.Models;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder): base(optionsBuilder)
        {

        }
    }
}
