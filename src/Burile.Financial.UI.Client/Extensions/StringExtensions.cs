namespace Burile.Financial.UI.Client.Extensions;

internal static class StringExtensions
{
    internal static Guid ToGuid(this string value)
        => Guid.Parse(value);
}