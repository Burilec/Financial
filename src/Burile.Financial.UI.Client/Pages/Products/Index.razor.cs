using Burile.Financial.Api.Client.Implementation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Products;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index
{
    private MudDataGrid<Product>? _dataGrid;

    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task<GridData<Product>> GetData(GridState<Product> state)
    {
        var result = await FinancialApiClient.RetrieveProductsAsync(state.Page + 1, state.PageSize)
                                             .ConfigureAwait(false);

        var productResponses = result.Value.Records;

        var gridData = new GridData<Product>
        {
            Items = productResponses.Select(static response => new Product(response)),
            TotalItems = result.Value.TotalRecords
        };

        return gridData;
    }

    private void EditProduct(Guid apiId)
    {
        NavigationManager.NavigateTo($"products/{apiId}");
    }
}