using System.Net;
using FluentResults;

namespace Burile.Financial.Api.Client.Errors;

internal sealed class HttpError : IError
{
    internal HttpError(HttpStatusCode statusCode, string json)
    {
        StatusCode = statusCode;
        Json = json;
    }

    public HttpStatusCode StatusCode { get; }
    public string Json { get; }

    public string? Message { get; } = null;
    public Dictionary<string, object>? Metadata { get; } = null;
    public List<IError>? Reasons { get; } = null;
}