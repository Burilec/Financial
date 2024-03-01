using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

public sealed class RetrieveProductRequest(RetrieveProductFilter? filter, RetrieveProductSort sort)
{
    public RetrieveProductFilter? Filter { get; } = filter;
    public RetrieveProductSort? Sort { get; } = sort;
}