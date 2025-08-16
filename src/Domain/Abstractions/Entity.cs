using Domain.Abstractions.Interfaces;

namespace Domain.Abstractions;
public abstract class Entity : IEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        MarkAsUpdated();
    }
}
public abstract class Entity<TId> : Entity, IEquatable<Entity<TId>>, IEntity<TId>
    where TId : notnull
{
    public TId Id { get; protected set; } = default!;

    protected Entity() { }
    protected Entity(TId id)
    {
        Id = id;
    }
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}