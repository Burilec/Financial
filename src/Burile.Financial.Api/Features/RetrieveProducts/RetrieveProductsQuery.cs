using Burile.Financial.Infrastructure;
using MediatR;

namespace Burile.Financial.Api.Features.RetrieveProducts;

public sealed class RetrieveProductsQuery(PagingOptions pagingOptions)
    : IRequest<PaginatedResult<RetrieveProductsResponse>>
{
    public PagingOptions PagingOptions { get; } = pagingOptions;
}