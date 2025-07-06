using Burile.Financial.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Burile.Financial.Infrastructure.Data.Factories;

// ReSharper disable once UnusedType.Global
public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinancialContext>
{
    public FinancialContext CreateDbContext(string[] args)
    {
        var configuration = ConfigurationFactory.CreateConfiguration();

        var optionsBuilder = new DbContextOptionsBuilder<FinancialContext>();
        var connectionString = configuration.GetConnectionString("Api");

        optionsBuilder.UseNpgsql(connectionString ?? throw new InvalidOperationException());

        return new(optionsBuilder.Options);
    }
}