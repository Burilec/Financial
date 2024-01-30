using Burile.Financial.Api.Client.Abstractions;
using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public sealed class FinancialApiClient(IHttpClientFactory httpFactory)
    : HttpClientBase, IFinancialApiClient
{
    public async Task<Result<PaginatedResult<RetrieveProductsResponse>>> RetrieveProductsAsync(
        CancellationToken cancellationToken = default)
    {
        const string uri = $"/api/products";

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