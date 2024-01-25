using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlDbContext<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? connectionString,
        ServiceLifetime? serviceLifetime,
        params Assembly[] interceptorsAssemblies) where TDbContext : DbContext
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string for {nameof(TDbContext)} was not found.");
        }

        var maxRetryCount = configuration.GetValue<int>("MySql:MaxRetryCount");

        services.AddDbContext<TDbContext>((provider, optionsBuilder) =>
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                                    mysqlBuilder => mysqlBuilder.EnableRetryOnFailure(maxRetryCount));

            var interceptors = provider.ResolveEfCoreInterceptors(interceptorsAssemblies);

            if (interceptors.Count != 0)
            {
                optionsBuilder.AddInterceptors(interceptors);
            }
        }, serviceLifetime ?? ServiceLifetime.Scoped);

        return services;
    }

    // public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services,
    //                                                            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    //     where TDbContext : DbContext
    // {
    //     services.TryAdd(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>), serviceLifetime));
    //
    //     return services;
    // }
}