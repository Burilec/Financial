namespace Burile.Financial.Core.Domain.Interfaces;

public interface ICreatableEntity
{
    public DateTime CreatedAt { get; set; }

    void SetCreationDateTime(DateTime createdAt);
}