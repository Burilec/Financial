using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.UpdateProduct;

internal static class ProductExtensions
{
    internal static Product Update(this Product product, UpdateProductCommand command)
        => product.Update(command.Name, command.Currency, command.Exchange, command.Country, command.MicCode);
}