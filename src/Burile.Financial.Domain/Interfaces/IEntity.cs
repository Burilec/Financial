namespace Burile.Financial.Domain.Interfaces;

internal interface IEntity<out TId> : ICreatableEntity, IModifiableEntity
{
    public TId Id { get; }
}

internal interface IEntity<out TId, out TApiId> : IEntity<TId>
{
    public TApiId ApiId { get; }
}