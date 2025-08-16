using Domain.Abstractions.StronglyTypedIds;

namespace Domain.Common.StronglyTypedIds;
public abstract record StronglyTypedGuidId<TId> : StronglyTypedId<Guid>
where TId : StronglyTypedGuidId<TId>, new()
{
    protected StronglyTypedGuidId() : base(Guid.Empty) { }

    protected StronglyTypedGuidId(Guid value) : base(value) { }

    public static TId NewId()
    {
        return new TId()
        {
            Value = Guid.CreateVersion7()
        };
    }

    public static TId Create(Guid value)
    {

        TId id = new()
        {
            Value = value
        };

        return id;
    }
}