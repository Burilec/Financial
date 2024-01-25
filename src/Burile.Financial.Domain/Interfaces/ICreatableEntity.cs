namespace Burile.Financial.Domain.Interfaces;

public interface ICreatableEntity
{
    public DateTime CreatedAt { get; }

    void SetCreationDateTime(DateTime createdAt);
}