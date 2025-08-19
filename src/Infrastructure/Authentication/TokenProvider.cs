using Application.Contracts.Authentication;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;
public class TokenProvider(IConfiguration _configuration) : ITokenProvider
{
    public string GenerateToken(User user)
    {
        string? key = _configuration["Jwt:Key"]!;
        string? issuer = _configuration["Jwt:Issuer"]!;
        string? audience = _configuration["Jwt:Audience"]!;
        int expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]!);
        DateTime? tokenExpires = DateTime.UtcNow.AddMinutes(expireMinutes);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress.Value),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];

        JwtSecurityToken? token = new(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: tokenExpires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}