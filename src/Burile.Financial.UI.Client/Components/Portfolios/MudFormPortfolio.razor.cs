using Burile.Financial.Api.Client.Implementation;
using Burile.Financial.Api.Client.Models;
using Burile.Financial.UI.Client.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Burile.Financial.UI.Client.Components.Portfolios;

public sealed partial class MudFormPortfolio
{
    private readonly PortfolioForm _form = new();
    private readonly PortfolioValidator _validator = new();
    private MudForm? _mudForm;
    private bool _processing;
    [Parameter] public string? ApiId { get; set; }
    [Parameter] public bool IsCreating { get; set; }
    [Parameter] public Action? OnSubmit { get; set; }
    [Inject] public IFinancialApiClient FinancialApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private async Task Submit()
    {
        _processing = true;
        if (_mudForm != null)
        {
            await _mudForm.Validate().ConfigureAwait(false);

            if (_mudForm.IsValid)
            {
                if (_form.ApiId == Guid.Empty)
                {
                    var request = new CreatePortfolioRequest(_form.Name ?? throw new InvalidOperationException());

                    await FinancialApiClient.CreatePortfolioAsync(request).ConfigureAwait(false);
                }
                else
                {
                    var request = new UpdatePortfolioRequest(_form.Name ?? throw new InvalidOperationException());

                    await FinancialApiClient.UpdatePortfolioAsync(_form.ApiId, request).ConfigureAwait(false);
                }

                _processing = false;
                OnSubmit?.Invoke();
                _form.Clear();
            }
        }

        _processing = false;
    }

    protected override async Task OnInitializedAsync()
    {
        if (ApiId != null)
        {
            var result = await FinancialApiClient.RetrievePortfolioAsync(ApiId.ToGuid()).ConfigureAwait(false);

            _form.ApiId = result.Value.ApiId;
            _form.Name = result.Value.Name;
        }
    }

    private void ReturnPage()
    {
        NavigationManager.NavigateTo($"portfolios");
    }
}

internal sealed class PortfolioForm
{
    public Guid ApiId { get; set; }
    public string? Name { get; set; }
}

internal static class PortfolioFormExtensions
{
    public static void Clear(this PortfolioForm portfolioForm)
    {
        portfolioForm.ApiId = Guid.Empty;
        portfolioForm.Name = string.Empty;
    }
}

internal sealed class PortfolioValidator : AbstractValidator<PortfolioForm>
{
    internal PortfolioValidator()
    {
        // RuleFor(static x => x.ApiId)
        //    .NotEmpty();
        RuleFor(static x => x.Name)
           .Length(0, 112);
    }

    internal Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(ValidationContext<PortfolioForm>.CreateWithOptions((PortfolioForm)model,
                                         x => x.IncludeProperties(propertyName)));
        return result.IsValid
            ? Array.Empty<string>()
            : result.Errors.Select<ValidationFailure, string>(static e => e.ErrorMessage);
    };
}