using Burile.Financial.Domain.Abstractions;

namespace Burile.Financial.Domain.Entities;

public sealed class Portfolio(string name) : AggregateRoot<long, Guid>(Guid.NewGuid())
{
    public string Name { get; private set; } = name;
    public bool IsRemoved { get; private set; }

    private void SetName(string name)
        => Name = name;

    public Portfolio Update(string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
            SetName(name);

        return this;
    }
}