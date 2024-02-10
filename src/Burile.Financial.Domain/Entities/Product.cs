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

    public void SetName(string name)
        => Name = name;

    public void SetCurrency(string currency)
        => Currency = currency;

    public void SetExchange(string exchange)
        => Exchange = exchange;

    public void SetCountry(string country)
        => Country = country;

    public void SetMicCode(string micCode)
        => MicCode = micCode;

    public override string ToString()
        => $"{base.ToString()}, " +
           $"{nameof(Symbol)}: {Symbol}, " +
           $"{nameof(Name)}: {Name}, " +
           $"{nameof(Currency)}: {Currency}, " +
           $"{nameof(Exchange)}: {Exchange}, " +
           $"{nameof(Country)}: {Country}, " +
           $"{nameof(MicCode)}: {MicCode}";

    public Product Update(string? name, string? currency, string? exchange, string? country, string? micCode)
    {
        if (!string.IsNullOrWhiteSpace(name))
            SetName(name);
        if (!string.IsNullOrWhiteSpace(currency))
            SetCurrency(currency);
        if (!string.IsNullOrWhiteSpace(exchange))
            SetExchange(exchange);
        if (!string.IsNullOrWhiteSpace(country))
            SetCountry(country);
        if (!string.IsNullOrWhiteSpace(micCode))
            SetMicCode(micCode);

        return this;
    }
}