namespace Shared.DTOs.Pokemons.Response;
public sealed record PokemonResponseDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; } = default!;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string ImageUrl { get; set; } = default!;
    public string Type1 { get; set; } = default!;
    public string? Type2 { get; set; }
    public bool IsCaught { get; set; }
}