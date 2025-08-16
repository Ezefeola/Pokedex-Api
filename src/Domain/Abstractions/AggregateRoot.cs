namespace Domain.Abstractions;
public abstract class AggregateRoot<TId> : Entity<TId>
where TId : notnull
{
    protected AggregateRoot() : base() { }
    protected AggregateRoot(TId id) : base(id)
    {

    }
}