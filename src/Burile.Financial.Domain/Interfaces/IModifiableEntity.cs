namespace Burile.Financial.Domain.Interfaces;

public interface IModifiableEntity
{
    public DateTime? ModifiedAt { get; }

    void SetModificationDateTime(DateTime modifiedAt);
}