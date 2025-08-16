using Domain.Common.StronglyTypedIds;

namespace Domain.Users.ValueObjects;
public sealed record UserId : StronglyTypedGuidId<UserId>
{
    public UserId()
    {

    }
}