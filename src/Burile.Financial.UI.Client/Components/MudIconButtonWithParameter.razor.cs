using Microsoft.AspNetCore.Components;

namespace Burile.Financial.UI.Client.Components;

public sealed partial class MudIconButtonWithParameter
{
    [Parameter] public Guid ApiId { get; set; }

    [Parameter] public Action<Guid>? OnAction { get; set; }

    public void OnClick()
    {
        OnAction?.Invoke(ApiId);
    }
}