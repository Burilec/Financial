using Burile.Financial.Domain.Interfaces;

namespace Burile.Financial.Domain.Abstractions;

public abstract class BaseEntity<TId> : IEntity<TId> where TId : struct
{
    public TId Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    public void SetCreationDateTime(DateTime createdAt) =>
        CreatedAt = createdAt;

    public void SetModificationDateTime(DateTime modifiedAt) =>
        ModifiedAt = modifiedAt;

    public override string ToString()
        => $"{nameof(Id)}: {Id}, "
         + $"{nameof(CreatedAt)}: {CreatedAt}, "
         + $"{nameof(ModifiedAt)}: {ModifiedAt}";
}

public abstract class BaseEntity<TId, TApiId>(TApiId apiId) : BaseEntity<TId>, IEntity<TId, TApiId>
    where TId : struct
    where TApiId : struct
{
    public TApiId ApiId { get; } = apiId;

    public override string ToString()
        => $"{base.ToString()}, " +
           $"{nameof(ApiId)}: {ApiId}";
}