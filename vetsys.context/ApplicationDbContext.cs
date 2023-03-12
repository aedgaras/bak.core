using Microsoft.EntityFrameworkCore;
using vetsys.entities.Models;

namespace vetsys.context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<CaseDiagnosis> CaseDiagnoses { get; set; }
    public DbSet<CaseResult> CaseResults { get; set; }
    public DbSet<MedicineRecipe> MedicineRecipes { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<ViewHistory> ViewHistories { get; set; }
}