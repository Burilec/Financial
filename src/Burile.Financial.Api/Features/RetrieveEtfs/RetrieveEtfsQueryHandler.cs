using Burile.Financial.TwelveData.Clients.Interfaces;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

public sealed class RetrieveEtfsQueryHandler(ITwelveDataClient twelveDataClient)
    : IRequestHandler<RetrieveEtfsQuery, string>
{
    public async Task<string> Handle(RetrieveEtfsQuery request, CancellationToken cancellationToken = default)
    {
        var etfs = await twelveDataClient.GetEtfsAsync(cancellationToken);

        return etfs;
    }
}