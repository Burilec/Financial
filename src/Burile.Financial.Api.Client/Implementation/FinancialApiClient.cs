using Burile.Financial.Api.Client.Abstractions;
using Burile.Financial.Api.Client.Models;
using FluentResults;

namespace Burile.Financial.Api.Client.Implementation;

public sealed class FinancialApiClient(IHttpClientFactory httpFactory)
    : HttpClientBase, IFinancialApiClient
{
    public async Task<Result<PaginatedResult<ProductResponse>>> RetrieveProductsAsync(
        int pageNumber, int pageSize, RetrieveProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products?PageNumber={pageNumber}&PageSize={pageSize}";

        try
        {
            return await PostAsync<RetrieveProductRequest, PaginatedResult<ProductResponse>>(uri, request,
                    cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<PaginatedResult<PortfolioResponse>>> RetrievePortfoliosAsync(
        int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/portfolios?PageNumber={pageNumber}&PageSize={pageSize}";

        try
        {
            return await GetAsync<PaginatedResult<PortfolioResponse>>(uri, cancellationToken)
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

    public async Task<Result<PortfolioResponse>> RetrievePortfolioAsync(
        Guid apiId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/portfolios/{apiId}";

        try
        {
            return await GetAsync<PortfolioResponse>(uri, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<ProductResponse>> UpdateProductAsync(Guid apiId, UpdateProductRequest request,
                                                                  CancellationToken cancellationToken = default)
    {
        var uri = $"/api/products/{apiId}";

        try
        {
            return await PutAsync<UpdateProductRequest, ProductResponse>(uri, request, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<PortfolioResponse>> UpdatePortfolioAsync(Guid apiId, UpdatePortfolioRequest request,
                                                                      CancellationToken cancellationToken = default)
    {
        var uri = $"/api/portfolios/{apiId}";

        try
        {
            return await PutAsync<UpdatePortfolioRequest, PortfolioResponse>(uri, request, cancellationToken)
               .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return Result.Fail(new ExceptionalError(exception));
        }
    }

    public async Task<Result<PortfolioResponse>> CreatePortfolioAsync(CreatePortfolioRequest request,
                                                                      CancellationToken cancellationToken = default)
    {
        const string uri = $"/api/portfolios";

        try
        {
            return await PostAsync<CreatePortfolioRequest, PortfolioResponse>(uri, request, cancellationToken)
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