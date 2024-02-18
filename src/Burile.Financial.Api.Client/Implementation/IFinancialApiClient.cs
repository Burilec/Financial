using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public interface IFinancialApiClient
{
    Task<Result<PaginatedResult<ProductResponse>>> RetrieveProductsAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<Result<PaginatedResult<PortfolioResponse>>> RetrievePortfoliosAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<Result<ProductResponse>> RetrieveProductAsync(Guid apiId, CancellationToken cancellationToken = default);
    Task<Result<PortfolioResponse>> RetrievePortfolioAsync(Guid apiId, CancellationToken cancellationToken = default);

    Task<Result<ProductResponse>> UpdateProductAsync(Guid apiId, UpdateProductRequest request,
                                                     CancellationToken cancellationToken = default);

    Task<Result<PortfolioResponse>> UpdatePortfolioAsync(Guid apiId, UpdatePortfolioRequest request,
                                                         CancellationToken cancellationToken = default);

    Task<Result<PortfolioResponse>> CreatePortfolioAsync(CreatePortfolioRequest request,
                                                         CancellationToken cancellationToken = default);
}