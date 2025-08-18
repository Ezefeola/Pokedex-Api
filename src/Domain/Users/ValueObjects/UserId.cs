using Domain.Abstractions.StronglyTypedIds;

namespace Domain.Users.ValueObjects;
public sealed record UserId : StronglyTypedGuidId<UserId>
{
    public UserId()
    {

    }
}