using Burile.Financial.Infrastructure;
using MediatR;

namespace Burile.Financial.Api.Features.RetrieveProducts;

public sealed class RetrieveProductsQuery(PagingOptions pagingOptions)
    : IRequest<PaginatedResult<RetrieveProductResponse>>
{
    public PagingOptions PagingOptions { get; } = pagingOptions;
}