namespace Burile.Financial.Core.Domain.Interfaces;

public interface IModifiableEntity
{
    public DateTime ModifiedAt { get; set; }

    void SetModificationDateTime(DateTime modifiedAt);
}