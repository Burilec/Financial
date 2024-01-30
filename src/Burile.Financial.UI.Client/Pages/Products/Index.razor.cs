using Burile.Financial.Api.Client.Models;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Products;

public partial class Index
{
    private MudDataGrid<RetrieveProductsResponse>? _dataGrid;

    private async Task<GridData<RetrieveProductsResponse>> GetData(GridState<RetrieveProductsResponse> state)
    {
        var statePage = state.Page == 0 ? 1 : state.Page;
        var statePageSize = state.PageSize;
        var result = await FinancialApiClient.RetrieveProductsAsync(statePage, statePageSize).ConfigureAwait(false);
        // _products = result.Value.Records.ToList();

        var gridData = new GridData<RetrieveProductsResponse>
        {
            Items = result.Value.Records.ToList(),
            TotalItems = result.Value.TotalRecords
        };

        return gridData;
    }
}