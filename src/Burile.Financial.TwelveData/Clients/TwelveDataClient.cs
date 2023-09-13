using Burile.Financial.TwelveData.Clients.Interfaces;
using Burile.Financial.TwelveData.Constants;
using Burile.Financial.TwelveData.Enums;

namespace Burile.Financial.TwelveData.Clients;

public sealed class TwelveDataClient : ITwelveDataClient
{
    private readonly HttpClient _httpClient;

    public TwelveDataClient(IHttpClientFactory httpClientFactory)
        => _httpClient = httpClientFactory.CreateClient(nameof(TwelveDataClient));

    public async Task<string> GetEtfsAsync()
        => await RequestApiAsync(ApiFunction.ETF).ConfigureAwait(false);

    private async Task<string> RequestApiAsync(ApiFunction function)
    {
        var httpRequestMessage = ComposeHttpRequest(function);

        var responseMessage = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

        var jsonString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

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