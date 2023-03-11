﻿using bak.models.Models;
using Microsoft.EntityFrameworkCore;

namespace bak.context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
}