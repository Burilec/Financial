using Burile.Financial.Api.Client.Abstractions;
using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public sealed class FinancialApiClient(IHttpClientFactory httpFactory)
    : HttpClientBase, IFinancialApiClient
{
    public async Task<Result<PaginatedResult<RetrieveProductsResponse>>> RetrieveProductsAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products?PageNumber={pageNumber}&PageSize={pageSize}";

        try
        {
            return await GetAsync<PaginatedResult<RetrieveProductsResponse>>(uri, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    protected override HttpClient CreateHttpClient()
        => httpFactory.CreateClient(nameof(FinancialApiClient));
}