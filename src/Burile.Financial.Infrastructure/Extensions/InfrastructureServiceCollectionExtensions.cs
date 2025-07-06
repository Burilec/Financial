using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext<TDbContext>(
        this IServiceCollection services,
        string? connectionString,
        ServiceLifetime? serviceLifetime,
        params Assembly[] interceptorsAssemblies) where TDbContext : DbContext
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string for {nameof(TDbContext)} was not found.");
        }

        services.AddDbContext<TDbContext>((provider, optionsBuilder) =>
        {
            optionsBuilder.UseNpgsql(connectionString ?? throw new InvalidOperationException());

            var interceptors = provider.ResolveEfCoreInterceptors(interceptorsAssemblies);

            if (interceptors.Count != 0)
            {
                optionsBuilder.AddInterceptors(interceptors);
            }
        }, serviceLifetime ?? ServiceLifetime.Scoped);

        return services;
    }
}