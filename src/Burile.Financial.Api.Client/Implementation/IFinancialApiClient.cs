using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public interface IFinancialApiClient
{
    Task<Result<PaginatedResult<ProductResponse>>> RetrieveProductsAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<Result<ProductResponse>> RetrieveProductAsync(Guid apiId, CancellationToken cancellationToken = default);

    Task<Result<ProductResponse>> UpdateProductAsync(Guid apiId, UpdateProductRequest updateProductRequest,
                                                     CancellationToken cancellationToken = default);
}