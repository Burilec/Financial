using Burile.Financial.AlphaVantage.Clients.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Burile.Financial.AlphaVantage.Clients;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAlphaVantageClient(
        this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<IAlphaVantageClient, AlphaVantageClient>();
}