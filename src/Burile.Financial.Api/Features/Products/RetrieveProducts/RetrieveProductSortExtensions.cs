using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;
using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure.Sorts;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

internal static class RetrieveProductSortExtensions
{
    internal static IEnumerable<SortInternal<Product, dynamic>> GetSortGuid(
        this RetrieveProductSort? retrieveProductSort)
    {
        if (retrieveProductSort?.ApiId != null)
        {
            yield return new(retrieveProductSort.ApiId.Index, retrieveProductSort.ApiId.SortType,
                             static product => product.ApiId);
        }
    }

    internal static IEnumerable<SortInternal<Product, dynamic>> GetSortString(
        this RetrieveProductSort? retrieveProductSort)
    {
        if (retrieveProductSort?.Symbol != null)
        {
            yield return new(retrieveProductSort.Symbol.Index, retrieveProductSort.Symbol.SortType,
                             static product => product.Symbol);
        }

        if (retrieveProductSort?.Name != null)
        {
            yield return new(retrieveProductSort.Name.Index, retrieveProductSort.Name.SortType,
                             static product => product.Name!);
        }

        if (retrieveProductSort?.Currency != null)
        {
            yield return new(retrieveProductSort.Currency.Index, retrieveProductSort.Currency.SortType,
                             static product => product.Currency!);
        }

        if (retrieveProductSort?.Exchange != null)
        {
            yield return new(retrieveProductSort.Exchange.Index, retrieveProductSort.Exchange.SortType,
                             static product => product.Exchange!);
        }

        if (retrieveProductSort?.Country != null)
        {
            yield return new(retrieveProductSort.Country.Index, retrieveProductSort.Country.SortType,
                             static product => product.Country!);
        }

        if (retrieveProductSort?.MicCode != null)
        {
            yield return new(retrieveProductSort.MicCode.Index, retrieveProductSort.MicCode.SortType,
                             static product => product.MicCode!);
        }
    }
}