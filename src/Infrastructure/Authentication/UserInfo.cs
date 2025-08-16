using Application.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication;
public class UserInfo : IUserInfo
{
    public UserInfo(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal? userClaims = httpContextAccessor.HttpContext?.User;
        if (userClaims == null)
        {
            throw new InvalidOperationException("User is not authenticated or is missing.");
        }

        string? sub = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? userClaims.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(sub))
        {
            throw new InvalidOperationException("User ID not found in the token.");
        }

        UserId = int.Parse(sub);
    }

    public int UserId { get; }
}