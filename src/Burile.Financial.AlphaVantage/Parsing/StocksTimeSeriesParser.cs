using System.Text.Json;
using FluentResults;

namespace Burile.Financial.AlphaVantage.Parsing;

internal static class StocksTimeSeriesParser
{
    private static readonly Dictionary<string, Action<StockDataPoint, string>?> ParsingDelegates =
        new()
        {
            { "open", static (dataPoint, strValue) => { dataPoint.OpeningPrice = strValue.ParseToDecimal(); } },
            { "high", static (dataPoint, strValue) => { dataPoint.HighestPrice = strValue.ParseToDecimal(); } },
            { "low", static (dataPoint, strValue) => { dataPoint.LowestPrice = strValue.ParseToDecimal(); } },
            { "close", static (dataPoint, strValue) => { dataPoint.ClosingPrice = strValue.ParseToDecimal(); } },
            { "volume", static (dataPoint, strValue) => { dataPoint.Volume = strValue.ParseToLong(); } },
            {
                "adjusted close",
                static (dataPoint, strValue) => { dataPoint.AdjustedClosingPrice = strValue.ParseToDecimal(); }
            },
            {
                "dividend amount",
                static (dataPoint, strValue) => { dataPoint.DividendAmount = strValue.ParseToDecimal(); }
            },
            {
                "split coefficient",
                static (dataPoint, strValue) => dataPoint.SplitCoefficient = strValue.ParseToDecimal()
            },
        };

    private static Action<StockDataPoint, string>? GetParsingDelegate(string fieldName) =>
        ParsingDelegates.GetValueOrDefault(fieldName);

    public static Result<StockTimeSeries> ParseApiResponse(string response)
    {
        var jsonDocument = JsonDocument.Parse(response);

        var stockTimeSeries = new StockTimeSeries();

        try
        {
            stockTimeSeries.MetaData = jsonDocument.ExtractMetaData();
            stockTimeSeries.DataPoints = GetDataPoints(jsonDocument);

            return stockTimeSeries.ToResult();
        }
        catch (Exception ex)
        {
            return Result.Fail(new ExceptionalError(ex));
        }
    }

    private static List<StockDataPoint> GetDataPoints(JsonDocument jsonDocument)
    {
        var result = new List<StockDataPoint>();

        var dataPointsJsonElement = jsonDocument.RootElement.EnumerateObject().Last().Value;

        foreach (var dataPointJson in dataPointsJsonElement.EnumerateObject())
        {
            var dataPoint = new StockDataPoint
            {
                Time = dataPointJson.Name.ParseToDateTime()
            };

            var dataPointFieldsJson = dataPointJson.Value;
            EnrichDataPointFields(dataPoint, dataPointFieldsJson);

            result.Add(dataPoint);
        }

        return result;
    }

    private static void EnrichDataPointFields(StockDataPoint dataPoint, JsonElement dataPointFieldsJson)
    {
        foreach (var fieldJson in dataPointFieldsJson.EnumerateObject())
            GetParsingDelegate(fieldJson.Name)?.Invoke(dataPoint, fieldJson.Value.GetString() ?? string.Empty);
    }
}