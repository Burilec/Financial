using Burile.Financial.Api.Client.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.Api.Client.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinancialApiClient(this IServiceCollection services, string baseUri)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHttpClient(nameof(FinancialApiClient),
                               configureClient => configureClient.BaseAddress = new(baseUri?.Trim('/', ' ') + '/'));

        services.AddTransient<IFinancialApiClient, FinancialApiClient>();

        return services;
    }
}