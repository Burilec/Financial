using Burile.Financial.TwelveData.Clients.Interfaces;
using Burile.Financial.TwelveData.Constants;
using Burile.Financial.TwelveData.Enums;

namespace Burile.Financial.TwelveData.Clients;

public sealed class TwelveDataClient(IHttpClientFactory httpClientFactory) : ITwelveDataClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(TwelveDataClient));

    public async Task<string> GetEtfsAsync(CancellationToken cancellationToken = default)
        => await RequestApiAsync(ApiFunction.ETF, cancellationToken).ConfigureAwait(false);

    private async Task<string> RequestApiAsync(ApiFunction function, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = ComposeHttpRequest(function);

        var responseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);

        var jsonString = await responseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return jsonString;
    }

    private static HttpRequestMessage ComposeHttpRequest(ApiFunction apiFunction)
    {
        var apiFunctionString = apiFunction.ToString().ToLowerInvariant();

        var url = string.Concat(ApiQueryConstants.TwelveDataUrl, apiFunctionString);

        var urlWithQuery = new Uri(url);

        var request = new HttpRequestMessage
        {
            RequestUri = urlWithQuery,
            Method = HttpMethod.Get
        };

        return request;
    }
}