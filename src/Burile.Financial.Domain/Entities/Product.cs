using Burile.Financial.Domain.Abstractions;

namespace Burile.Financial.Domain.Entities;

public sealed class Product(string symbol) : AggregateRoot<long, Guid>(Guid.NewGuid())
{
    public string Symbol { get; private init; } = symbol;
    public string? Name { get; private set; }
    public string? Currency { get; private set; }
    public string? Exchange { get; private set; }
    public string? Country { get; private set; }
    public string? MicCode { get; private set; }
    public bool IsRemoved { get; private set; }
    public bool Track { get; private set; }

    public ICollection<MonthlyQuote> MonthlyQuotes { get; private set; }
    public DateTime LastUpdateMonthlyQuotes { get; private set; }

    public Product Update(string? name, string? currency, string? exchange, string? country, string? micCode)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(currency))
            Currency = currency;
        if (!string.IsNullOrWhiteSpace(exchange))
            Exchange = exchange;
        if (!string.IsNullOrWhiteSpace(country))
            Country = country;
        if (!string.IsNullOrWhiteSpace(micCode))
            MicCode = micCode;

        return this;
    }

    public override string ToString()
        => $"{base.ToString()}, " +
           $"{nameof(Symbol)}: {Symbol}, " +
           $"{nameof(Name)}: {Name}, " +
           $"{nameof(Currency)}: {Currency}, " +
           $"{nameof(Exchange)}: {Exchange}, " +
           $"{nameof(Country)}: {Country}, " +
           $"{nameof(MicCode)}: {MicCode}";
}