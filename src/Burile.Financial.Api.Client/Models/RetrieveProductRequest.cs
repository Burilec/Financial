using Burile.Financial.Api.Client.Models.Filters;
using Burile.Financial.Api.Client.Models.Sorts;

namespace Burile.Financial.Api.Client.Models;

public sealed class RetrieveProductRequest(RetrieveProductFilter? filter, RetrieveProductSort? sort)
{
    public RetrieveProductFilter? Filter { get; } = filter;
    public RetrieveProductSort? Sort { get; } = sort;
}