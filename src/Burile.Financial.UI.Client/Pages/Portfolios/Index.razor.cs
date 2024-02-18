using Burile.Financial.Api.Client.Implementation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Portfolios;

public sealed partial class Index
{
    private MudDataGrid<Portfolio>? _dataGrid;
    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task<GridData<Portfolio>> GetData(GridState<Portfolio> state)
    {
        var result = await FinancialApiClient.RetrievePortfoliosAsync(state.Page, state.PageSize)
                                             .ConfigureAwait(false);

        var portfoliosResponse = result.Value.Records;

        var gridData = new GridData<Portfolio>
        {
            Items = portfoliosResponse.Select(static portfolio => new Portfolio(portfolio)),
            TotalItems = result.Value.TotalRecords
        };

        return gridData;
    }

    private void Edit(Guid apiId)
    {
        NavigationManager.NavigateTo($"portfolios/{apiId}");
    }

    private void OnSubmit()
    {
        _dataGrid?.ReloadServerData();
    }
}