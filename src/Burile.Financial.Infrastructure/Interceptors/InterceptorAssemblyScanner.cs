using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.Infrastructure.Interceptors;

internal static class InterceptorAssemblyScanner
{
    internal static IEnumerable<IInterceptor> Scan(IServiceProvider? serviceProvider = null,
                                                   params Assembly[] interceptorAssemblies)
    {
        if (interceptorAssemblies.Length == 0)
        {
            return Enumerable.Empty<IInterceptor>();
        }

        return interceptorAssemblies
              .Distinct()
              .SelectMany(static assembly => assembly.GetTypes())
              .Where(static type => type is { IsClass: true, IsAbstract: false } &&
                                    type.IsAssignableTo(typeof(IInterceptor)) &&
                                    type.GetConstructor(
                                                        BindingFlags.Instance | BindingFlags.Public,
                                                        null,
                                                        Type.EmptyTypes,
                                                        null) is not null)
              .Select(type => serviceProvider is null
                          ? Activator.CreateInstance(type)
                          : ActivatorUtilities.CreateInstance(serviceProvider, type))
              .Cast<IInterceptor>();
    }
}