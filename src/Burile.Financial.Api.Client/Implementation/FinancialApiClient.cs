using Burile.Financial.Api.Client.Abstractions;
using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public sealed class FinancialApiClient(IHttpClientFactory httpFactory)
    : HttpClientBase, IFinancialApiClient
{
    public async Task<Result<PaginatedResult<ProductResponse>>> RetrieveProductsAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products?PageNumber={pageNumber}&PageSize={pageSize}";

        try
        {
            return await GetAsync<PaginatedResult<ProductResponse>>(uri, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<ProductResponse>> RetrieveProductAsync(
        Guid apiId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products/{apiId}";

        try
        {
            return await GetAsync<ProductResponse>(uri, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<ProductResponse>> UpdateProductAsync(Guid apiId,
                                                                  UpdateProductRequest updateProductRequest,
                                                                  CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products/{apiId}";

        try
        {
            return await PutAsync<UpdateProductRequest, ProductResponse>(uri, updateProductRequest, cancellationToken)
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