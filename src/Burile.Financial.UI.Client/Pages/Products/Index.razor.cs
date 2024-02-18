using Burile.Financial.Api.Client.Implementation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Products;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class Index
{
    private MudDataGrid<Product>? _dataGrid;

    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task<GridData<Product>> GetData(GridState<Product> state)
    {
        // if (state.FilterDefinitions is { Count: > 0 })
        // {
        //     foreach (var filter in state.FilterDefinitions)
        //     {
        //         var expression = FilterExpressionGenerator.GenerateExpression<Product>
        //             (filter, new FilterOptions() { FilterCaseSensitivity = DataGridFilterCaseSensitivity.Default });
        //         // query = query.Where(expression);
        //     }
        // }
        //
        // if (state.SortDefinitions is { Count: > 0 })
        // {
        //     foreach (var sort in state.SortDefinitions)
        //     {
        //         // query = query.OrderBy($"{sort.SortBy} {(sort.Descending ? "descending" : "ascending")}");
        //     }
        // }
        // else
        // {
        //     // query = query.OrderBy(w => w.Date);
        // }

        var result = await FinancialApiClient.RetrieveProductsAsync(state.Page, state.PageSize)
                                             .ConfigureAwait(false);

        var productsResponse = result.Value.Records;

        var gridData = new GridData<Product>
        {
            Items = productsResponse.Select(static response => new Product(response)),
            TotalItems = result.Value.TotalRecords
        };

        return gridData;
    }

    private void Edit(Guid apiId)
    {
        NavigationManager.NavigateTo($"products/{apiId}");
    }
}