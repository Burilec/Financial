using Burile.Financial.AlphaVantage.Enums;
using FluentResults;

namespace Burile.Financial.AlphaVantage.Clients.Interfaces;

internal interface IAlphaVantageClient
{
    Task<Result<StockTimeSeries>> GetTimeSeriesAsync(string apiKey, string symbol, Interval interval,
                                                     bool isAdjusted = false,
                                                     CancellationToken cancellationToken = default);
}