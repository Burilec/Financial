namespace Burile.Financial.Domain.Entities;

public sealed class MonthlyQuote(
    DateTime dateTime,
    decimal openingPrice,
    decimal closingPrice,
    decimal highestPrice,
    decimal lowestPrice,
    decimal adjustedClose,
    long volume,
    decimal dividendAmount)
    : Quote(dateTime,
            openingPrice,
            closingPrice,
            highestPrice,
            lowestPrice,
            adjustedClose,
            volume,
            dividendAmount);