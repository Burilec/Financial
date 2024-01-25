using System.Reflection;
using Burile.Financial.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.Infrastructure.Extensions;

internal static class InfrastructureServiceProviderExtensions
{
    internal static ICollection<IInterceptor> ResolveEfCoreInterceptors(
        this IServiceProvider serviceProvider,
        params Assembly[] interceptorsAssemblies)
    {
        var scannedInterceptors = InterceptorAssemblyScanner.Scan(serviceProvider, interceptorsAssemblies).ToList();

        return scannedInterceptors.Count == 0
            ? []
            : scannedInterceptors;
    }

    public static async Task ApplyMigrationsAsync<TDbContext>(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default)
        where TDbContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService<TDbContext>();

        if (context is null)
        {
            throw new InvalidOperationException($"{nameof(TDbContext)} cannot be resolved.");
        }

        await context.Database.MigrateAsync(cancellationToken);
    }
}