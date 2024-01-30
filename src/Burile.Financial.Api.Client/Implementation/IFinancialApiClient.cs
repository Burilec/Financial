using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public interface IFinancialApiClient
{
    Task<Result<PaginatedResult<RetrieveProductsResponse>>> RetrieveProductsAsync(
        CancellationToken cancellationToken = default);
}