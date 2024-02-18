using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure.Data.Contexts;
using Burile.Financial.TwelveData.Clients.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Burile.Financial.Api.Features.UpdateInformationEtfs;

public sealed class UpdateInformationEtfsCommandHandler(
    ITwelveDataClient twelveDataClient,
    FinancialContext financialContext)
    : IRequestHandler<UpdateInformationEtfsCommand>
{
    public async Task Handle(UpdateInformationEtfsCommand request, CancellationToken cancellationToken)
    {
        var etfsRaw = await twelveDataClient.GetEtfsAsync(cancellationToken).ConfigureAwait(false);

        var etfs = JsonConvert.DeserializeObject<UpdateInformationEtfsDto>(etfsRaw);

        if (etfs?.Data == null)
        {
            //todo:Better check
            return;
        }

        var products = await financialContext.Products
                                             .Where(product => etfs.Data
                                                                   .Select(static dto => dto.Symbol)
                                                                   .Contains(product.Symbol))
                                             .ToListAsync(cancellationToken)
                                             .ConfigureAwait(false);

        var updates = etfs.Data
                          .Where(static dto => dto.Symbol != null)
                          .Select(dto => (products.FirstOrDefault(_ => _.Symbol == dto.Symbol)
                                       ?? new Product(dto.Symbol!))
                                     .Update(dto.Name, dto.Currency, dto.Exchange, dto.Country, dto.MicCode));

        financialContext.Products.UpdateRange(updates);

        await financialContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}