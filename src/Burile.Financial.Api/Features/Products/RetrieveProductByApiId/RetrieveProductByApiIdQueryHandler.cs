using Burile.Financial.Infrastructure.Data.Contexts;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Products.RetrieveProductByApiId;

public sealed class RetrieveProductByApiIdQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrieveProductByApiIdQuery, Result<RetrieveProductByApiIdResponse>>
{
    public async Task<Result<RetrieveProductByApiIdResponse>> Handle(RetrieveProductByApiIdQuery request,
                                                                     CancellationToken cancellationToken)
    {
        var product = await financialContext.Products
                                            .Where(product => product.ApiId == request.ApiId)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return product == null
            ? Result.Fail("Not found")
            : RetrieveProductByApiIdResponse.FromProduct(product).ToResult();
    }
}