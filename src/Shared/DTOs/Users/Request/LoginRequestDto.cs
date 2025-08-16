namespace Shared.DTOs.Users.Request;
public sealed record LoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}