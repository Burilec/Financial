using Burile.Financial.Api.Client.Implementation;
using Burile.Financial.Api.Client.Models;
using Burile.Financial.Api.Client.Models.Filters;
using Burile.Financial.Api.Client.Models.Sorts;
using Burile.Financial.UI.Client.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Products;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class Index
{
    private MudDataGrid<Product>? _dataGrid;
    private IEnumerable<Product>? _internalProducts;
    private ICollection<IFilterDefinition<Product>>? _previousFilterDefinitions;

    private int _previousPage = 0;

    // private ICollection<SortDefinition<Product>>? _previousSortDefinitions;
    private int _previousTotalItems = 0;

    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task<GridData<Product>> GetData(GridState<Product> state)
    {
        // if (!state.FilterDefinitions.Equal(_previousFilterDefinitions))
        // {
        //     _previousFilterDefinitions = state.FilterDefinitions.Clone().ToList();
        //     state.Page = 0;
        // }
        // else
        // {
        //     if (_previousPage != state.Page)
        //     {
        //         _previousPage = state.Page;
        //     }
        //     else
        //     {
        //         if (_internalProducts is not null)
        //         {
        //             return new()
        //             {
        //                 Items = _internalProducts,
        //                 TotalItems = _previousTotalItems
        //             };
        //         }
        //     }
        // }

        var result = await FinancialApiClient.RetrieveProductsAsync(state.Page, state.PageSize, GetRequest(state))
                                             .ConfigureAwait(false);

        var productsResponse = result.Value.Records;

        var gridData = new GridData<Product>
        {
            Items = _internalProducts = productsResponse.Select(static response => new Product(response)).ToList(),
            TotalItems = _previousTotalItems = result.Value.TotalRecords
        };

        return gridData;
    }

    private static RetrieveProductRequest GetRequest(GridState<Product> state)
        => new(GetFilter(state.FilterDefinitions), GetSort(state.SortDefinitions));

    private static RetrieveProductSort GetSort(ICollection<SortDefinition<Product>> stateSortDefinitions)
        => new(stateSortDefinitions.GenerateSortApi(nameof(Product.ApiId)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.Symbol)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.Name)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.Currency)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.Exchange)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.Country)),
               stateSortDefinitions.GenerateSortApi(nameof(Product.MicCode)));

    private static RetrieveProductFilter? GetFilter(
        ICollection<IFilterDefinition<Product>> filterDefinitions)
        => filterDefinitions is not { Count: > 0 }
            ? null
            : new(filterDefinitions.GenerateFilterApi(nameof(Product.ApiId))?.Cast<FilterGuid>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.Symbol))?.Cast<FilterString>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.Name))?.Cast<FilterString>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.Currency))?.Cast<FilterString>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.Exchange))?.Cast<FilterString>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.Country))?.Cast<FilterString>(),
                  filterDefinitions.GenerateFilterApi(nameof(Product.MicCode))?.Cast<FilterString>());

    private void Edit(Guid apiId)
        => NavigationManager.NavigateTo($"products/{apiId}");
}