using FluentResults;

namespace Burile.Financial.Api.Client.Errors;

internal sealed class DeserializeError(string json, Type type) : IError
{
    public string Json { get; } = json;
    public Type Type { get; } = type;
    public string? Message { get; } = null;
    public Dictionary<string, object>? Metadata { get; } = null;
    public List<IError>? Reasons { get; } = null;
}