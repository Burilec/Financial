using Burile.Financial.TwelveData.Clients;
using Burile.Financial.TwelveData.Clients.Interfaces;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

public static class RetrieveEtfsServiceCollectionExtension
{
    public static IServiceCollection AddRetrieveEtfsServices(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<ITwelveDataClient, TwelveDataClient>();
}