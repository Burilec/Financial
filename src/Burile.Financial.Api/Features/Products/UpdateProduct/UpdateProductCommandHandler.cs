using Burile.Financial.Infrastructure.Data.Contexts;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(FinancialContext financialContext)
    : IRequestHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await financialContext.Products
                                            .FirstOrDefaultAsync(p => p.ApiId == request.ApiId, cancellationToken)
                                            .ConfigureAwait(false);

        if (product == null)
        {
            return Result.Fail("Product not found");
        }

        financialContext.Products.Update(product.Update(request));

        await financialContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}