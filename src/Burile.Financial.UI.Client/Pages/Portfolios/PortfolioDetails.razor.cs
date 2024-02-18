using Microsoft.AspNetCore.Components;

namespace Burile.Financial.UI.Client.Pages.Portfolios;

public sealed partial class PortfolioDetails
{
    [Parameter] public string? ApiId { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private void ReturnPage()
    {
        NavigationManager.NavigateTo($"portfolios");
    }

    private void OnSubmit()
    {
        ReturnPage();
    }
}