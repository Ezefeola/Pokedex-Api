namespace Shared.DTOs.Users.Response;
public sealed record LoginResponseDto
{
    public string Token { get; set; } = default!;
}