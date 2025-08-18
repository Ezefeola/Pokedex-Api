namespace Shared.DTOs.Users.Request;
public sealed record RegisterRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; } 
    public required string Password { get; set; }
}