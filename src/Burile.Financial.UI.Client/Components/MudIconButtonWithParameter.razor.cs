using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Burile.Financial.UI.Client.Components;

public sealed partial class MudIconButtonWithParameter : MudIconButton
{
    [Parameter] public Guid ApiId { get; set; }

    [Parameter] public Action<Guid>? OnAction { get; set; }

    protected override Task OnClickHandler(MouseEventArgs ev)
    {
        OnClickAction();
        return base.OnClickHandler(ev);
    }

    private void OnClickAction()
        => OnAction?.Invoke(ApiId);
}