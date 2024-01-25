using System.Text.RegularExpressions;
using Burile.Financial.AlphaVantage.Clients.Interfaces;
using Burile.Financial.AlphaVantage.Constants;
using Burile.Financial.AlphaVantage.Enums;
using Burile.Financial.AlphaVantage.Extensions;
using Burile.Financial.AlphaVantage.Parsing;
using FluentResults;
using Microsoft.AspNetCore.WebUtilities;

namespace Burile.Financial.AlphaVantage.Clients;

public sealed partial class AlphaVantageClient(IHttpClientFactory httpClientFactory) : IAlphaVantageClient
{
    private const string BadRequestToken = "Error Message";

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(AlphaVantageClient));

    public async Task<Result<StockTimeSeries>> GetTimeSeriesAsync(string apiKey, string symbol, Interval interval,
                                                                  bool isAdjusted = false,
                                                                  CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            { ApiQueryConstants.SymbolQueryVar, symbol },
            { ApiQueryConstants.OutputSizeQueryVar, "FULL" }
        };

        var function = interval.ConvertToApiFunction(isAdjusted);

        if (function == ApiFunction.TIME_SERIES_INTRADAY)
        {
            query.Add(ApiQueryConstants.IntervalQueryVar, interval.ConvertToQueryString());
        }

        var requestApiAsync = await RequestApiAsync(apiKey, function, query, cancellationToken).ConfigureAwait(false);

        if (requestApiAsync.Contains(BadRequestToken))
        {
            return Result.Fail(requestApiAsync);
        }

        requestApiAsync = CleanJsonFromSequenceNumbers(requestApiAsync);

        var stockDataPoint = StocksTimeSeriesParser.ParseApiResponse(requestApiAsync);

        return stockDataPoint;
    }

    private static string CleanJsonFromSequenceNumbers(string jsonString)
        => MyRegex().Replace(jsonString, "\"");

    [GeneratedRegex("\"(\\d+)(\\.)?(\\d+)?[a-z]?[\\.\\:]\\s", RegexOptions.Multiline)]
    private static partial Regex MyRegex();

    private async Task<string> RequestApiAsync(string apiKey, ApiFunction function,
                                               IDictionary<string, string?> query,
                                               CancellationToken cancellationToken = default)
    {
        var request = ComposeHttpRequest(apiKey, function, query);

        var response = await _httpClient.SendAsync(request, cancellationToken)
                                        .ConfigureAwait(false);
        var jsonString = await response.Content.ReadAsStringAsync(cancellationToken)
                                       .ConfigureAwait(false);

        return jsonString;
    }

    private static HttpRequestMessage ComposeHttpRequest(string apiKey, ApiFunction function,
                                                         IDictionary<string, string?> query)
    {
        var fullQueryDict = new Dictionary<string, string?>(query)
        {
            { ApiQueryConstants.ApiKeyQueryVar, apiKey },
            { ApiQueryConstants.FunctionQueryVar, function.ToString() }
        };

        var urlWithQueryString = QueryHelpers.AddQueryString(ApiQueryConstants.AlfaVantageUrl, fullQueryDict);
        var urlWithQuery = new Uri(urlWithQueryString);

        var request = new HttpRequestMessage
        {
            RequestUri = urlWithQuery,
            Method = HttpMethod.Get
        };

        return request;
    }
}