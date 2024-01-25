using Burile.Financial.Domain.Interfaces;
using MediatR;

namespace Burile.Financial.Domain.Abstractions;

public abstract class AggregateRoot<TId, TApiId>(TApiId apiId)
    : BaseEntity<TId, TApiId>(apiId), IAggregateRoot where TApiId : struct where TId : struct
{
    private readonly List<INotification> _domainEvents = [];

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(INotification domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}