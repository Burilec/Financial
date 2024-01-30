using System.Text.Json;
using Burile.Financial.Api.Client.Errors;
using FluentResults;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Burile.Financial.Api.Client.Abstractions;

public abstract class HttpClientBase
{
    private readonly JsonSerializerOptions _options = CreateOptions();

    private static JsonSerializerOptions CreateOptions()
        => new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };

    protected async Task<Result<TResponse>> GetAsync<TResponse>(
        string uri, CancellationToken cancellationToken = default)
    {
        var httpClient = CreateHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Omit);
        var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var result = await HandleResponse(response, cancellationToken).ConfigureAwait(false);
        return result.IsFailed ? result.ToResult() : FromJson<TResponse>(result.Value);
    }

    protected async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(
        string uri, TRequest data, CancellationToken cancellationToken)
    {
        var httpClient = CreateHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var json = ToJson(data);
        request.Content = ToHttpContent(json);
        var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var result = await HandleResponse(response, cancellationToken).ConfigureAwait(false);
        return result.IsFailed ? result.ToResult() : FromJson<TResponse>(result.Value);
    }

    protected async Task<Result<TResponse>> PutAsync<TRequest, TResponse>(
        string uri, TRequest data, CancellationToken cancellationToken)
    {
        var httpClient = CreateHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        var json = ToJson(data);
        request.Content = ToHttpContent(json);
        var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var result = await HandleResponse(response, cancellationToken).ConfigureAwait(false);
        return result.IsFailed ? result.ToResult() : FromJson<TResponse>(result.Value);
    }

    private static StringContent ToHttpContent(string json)
    {
        var stringContent = new StringContent(json);
        stringContent.Headers.ContentType = new("application/json");
        return stringContent;
    }


    private string ToJson<TRequest>(TRequest request)
        => JsonSerializer.Serialize(request, _options);

    private Result<TResponse> FromJson<TResponse>(string json)
    {
        var deserialize = JsonSerializer.Deserialize<TResponse>(json, _options);

        return deserialize is null
            ? Result.Fail(new DeserializeError(json, typeof(TResponse)))
            : Result.Ok(deserialize);
    }

    private static async Task<Result<string>> HandleResponse(HttpResponseMessage response,
                                                             CancellationToken cancellationToken)
    {
        var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return response.IsSuccessStatusCode
            ? Result.Ok(json)
            : Result.Fail(new HttpError(response.StatusCode, json));
    }

    protected abstract HttpClient CreateHttpClient();
}