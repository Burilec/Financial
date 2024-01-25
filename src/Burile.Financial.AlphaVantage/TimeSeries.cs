namespace Burile.Financial.AlphaVantage;

public sealed class StockTimeSeries
{
    public ICollection<StockDataPoint> DataPoints { get; set; } = new List<StockDataPoint>();

    public Dictionary<string, string> MetaData { get; set; } = new();
}