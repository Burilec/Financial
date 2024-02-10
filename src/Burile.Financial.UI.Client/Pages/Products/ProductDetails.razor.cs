using Burile.Financial.Api.Client.Implementation;
using Burile.Financial.Api.Client.Models;
using Burile.Financial.UI.Client.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Pages.Products;

public sealed partial class ProductDetails
{
    private readonly ProductForm _form = new();
    private readonly ProductValidator _productValidator = new();
    private MudForm? _mudForm;
    [Parameter] public string? ApiId { get; set; }
    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task Submit()
    {
        if (_mudForm != null)
        {
            await _mudForm.Validate();

            if (_mudForm.IsValid)
            {
                var updateProductRequest =
                    new UpdateProductRequest(_form.Name, _form.Currency, _form.Exchange, _form.Country, _form.MicCode);

                await FinancialApiClient.UpdateProductAsync(_form.ApiId!.ToGuid(), updateProductRequest)
                                        .ConfigureAwait(false);

                NavigationManager.NavigateTo($"products");
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (ApiId != null)
        {
            var result = await FinancialApiClient.RetrieveProductAsync(ApiId.ToGuid()).ConfigureAwait(false);

            var product = result.Value;

            _form.ApiId = product.ApiId.ToString();
            _form.Symbol = product.Symbol;
            _form.Name = result.Value.Name;
            _form.Currency = result.Value.Currency;
            _form.Exchange = result.Value.Exchange;
            _form.Country = result.Value.Country;
            _form.MicCode = result.Value.MicCode;
        }
    }
}