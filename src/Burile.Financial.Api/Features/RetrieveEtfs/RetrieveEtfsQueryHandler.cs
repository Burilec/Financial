using Burile.Financial.TwelveData.Clients.Interfaces;
using MediatR;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

public sealed class RetrieveEtfsQueryHandler : IRequestHandler<RetrieveEtfsQuery, string>
{
    private readonly ITwelveDataClient _twelveDataClient;

    public RetrieveEtfsQueryHandler(ITwelveDataClient twelveDataClient)
        => _twelveDataClient = twelveDataClient;

    public async Task<string> Handle(RetrieveEtfsQuery request, CancellationToken cancellationToken = default)
    {
        var etfs = await _twelveDataClient.GetEtfsAsync(cancellationToken);

        return etfs;
    }
}