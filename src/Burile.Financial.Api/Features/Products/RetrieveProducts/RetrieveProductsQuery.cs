using Burile.Financial.Infrastructure;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

public sealed class RetrieveProductsQuery(PagingOptions pagingOptions, RetrieveProductRequest? retrieveProductRequest)
    : IRequest<PaginatedResult<RetrieveProductResponse>>
{
    public PagingOptions PagingOptions { get; } = pagingOptions;
    public RetrieveProductRequest? RetrieveProductRequest { get; } = retrieveProductRequest;
}