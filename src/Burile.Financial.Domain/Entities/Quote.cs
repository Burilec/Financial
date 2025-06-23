using Burile.Financial.Domain.Abstractions;
using Microsoft.VisualBasic.FileIO;

namespace Burile.Financial.Domain.Entities;

public class Quote(
    DateTime dateTime,
    decimal openingPrice,
    decimal closingPrice,
    decimal highestPrice,
    decimal lowestPrice,
    decimal adjustedClose,
    long volume,
    decimal dividendAmount) : BaseEntity<long, Guid>(Guid.NewGuid())

{
    public DateTime DateTime { get; } = dateTime;
    public decimal OpeningPrice { get; } = openingPrice;
    public decimal ClosingPrice { get; } = closingPrice;
    public decimal HighestPrice { get; } = highestPrice;
    public decimal LowestPrice { get; } = lowestPrice;
    public decimal AdjustedClose { get; } = adjustedClose;
    public long Volume { get; set; } = volume;
    public decimal DividendAmount { get; } = dividendAmount;
}