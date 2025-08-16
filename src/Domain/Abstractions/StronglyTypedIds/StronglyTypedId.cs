using Domain.Abstractions.ValueObjects;

namespace Domain.Abstractions.StronglyTypedIds;
public abstract record StronglyTypedId<TId> : ValueObject
where TId : notnull
{
    private StronglyTypedId() { }

    protected StronglyTypedId(TId value)
    {
        Value = value;
    }

    public TId Value { get; init; } = default!;

    public override string ToString() => Value.ToString() ?? string.Empty;
}