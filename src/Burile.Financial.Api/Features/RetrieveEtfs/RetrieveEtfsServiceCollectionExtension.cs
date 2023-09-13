using Burile.Financial.TwelveData.Clients;
using Burile.Financial.TwelveData.Clients.Interfaces;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

internal static class RetrieveEtfsServiceCollectionExtension
{
    internal static IServiceCollection AddRetrieveEtfsServices(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<ITwelveDataClient, TwelveDataClient>();
}