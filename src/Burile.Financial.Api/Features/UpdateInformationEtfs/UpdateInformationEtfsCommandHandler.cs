using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure.Data.Contexts;
using Burile.Financial.TwelveData.Clients.Interfaces;
using MediatR;
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
        try
        {
            var etfs = await twelveDataClient.GetEtfsAsync(cancellationToken).ConfigureAwait(false);

            //todo:Check null
            var updateInformationEtfsDto = JsonConvert.DeserializeObject<UpdateInformationEtfsDto>(etfs);

            var products = await financialContext.Products
                                                 .Where(product => updateInformationEtfsDto.Data
                                                           .Select(static dto => dto.Symbol)
                                                           .Contains(product.Symbol))
                                                 .ToListAsync(cancellationToken)
                                                 .ConfigureAwait(false);

            foreach (var dto in updateInformationEtfsDto.Data)
            {
                if (dto.Symbol == null)
                    continue;

                var product = products.FirstOrDefault(_ => _.Symbol == dto.Symbol) ?? new Product(dto.Symbol);

                product.Update(dto.Name, dto.Currency, dto.Exchange, dto.Country, dto.MicCode);

                financialContext.Products.Update(product);
            }

            await financialContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}