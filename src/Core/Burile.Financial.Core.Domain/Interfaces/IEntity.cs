namespace Burile.Financial.Core.Domain.Interfaces;

public interface IEntity<TId, TApiId> : ICreatableEntity, IModifiableEntity
{
    public TId Id { get; set; }
    public TApiId ApiId { get; set; }
}