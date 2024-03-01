namespace Burile.Financial.UI.Client.Extensions;

internal static class StringExtensions
{
    internal static Guid ToGuid(this string? value)
        => !string.IsNullOrWhiteSpace(value) ? Guid.Parse(value) : Guid.Empty;
}