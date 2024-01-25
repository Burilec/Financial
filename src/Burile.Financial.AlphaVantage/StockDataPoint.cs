namespace Burile.Financial.AlphaVantage;

public sealed class StockDataPoint
{
    public DateTime Time { get; set; }
    public decimal OpeningPrice { get; set; }
    public decimal ClosingPrice { get; set; }
    public decimal HighestPrice { get; set; }
    public decimal LowestPrice { get; set; }
    public long Volume { get; set; }
    public decimal? AdjustedClosingPrice { get; set; }
    public decimal? DividendAmount { get; set; }
    public decimal? SplitCoefficient { get; set; }
}