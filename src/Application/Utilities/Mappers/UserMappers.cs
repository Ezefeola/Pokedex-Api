using Domain.Users;
using Shared.DTOs.Users.Response;

namespace Application.Utilities.Mappers;
public static class UserMappers
{
    public static UserResponseDto ToUserResponseDto(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id.Value,
            FirstName = user.FullName.FirstName,
            LastName = user.FullName.LastName,
            Email = user.EmailAddress.Value,
            Role = user.Role.ToString(),
        };
    }
}