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

        optionsBuilder.UseMySql(connectionString ?? throw new InvalidOperationException(),
                                ServerVersion.AutoDetect(connectionString),
                                static x => x.MigrationsAssembly(typeof(FinancialContext).Assembly.GetName().Name));

        return new FinancialContext(optionsBuilder.Options);
    }
}