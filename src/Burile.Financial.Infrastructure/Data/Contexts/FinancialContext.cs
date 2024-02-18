using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Burile.Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Infrastructure.Data.Contexts;

public sealed class FinancialContext : DbContext
{
    public FinancialContext(DbContextOptions<FinancialContext> options)
        : base(options)
        => ChangeTracker.LazyLoadingEnabled = false;

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Portfolio> Portfolios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<string>()
                            .HaveMaxLength(36);

        configurationBuilder.Properties<decimal>()
                            .HavePrecision(60, 30);
    }

#if DEBUG
    [ExcludeFromCodeCoverage]
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(static message => Debug.WriteLine(message))
                      .EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }
#endif
}