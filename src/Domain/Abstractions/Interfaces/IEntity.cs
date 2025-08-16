namespace Domain.Abstractions.Interfaces;
public interface IEntity
{
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedAt { get; }

    public void MarkAsUpdated();
    public void SoftDelete();
    public void Restore();
}
public interface IEntity<TId> : IEntity
{
    public TId Id { get; }
}