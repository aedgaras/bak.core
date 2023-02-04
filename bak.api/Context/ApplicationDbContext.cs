using bak.api.Models;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Case> Cases { get; set; }
}