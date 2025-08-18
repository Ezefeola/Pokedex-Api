namespace Shared.DTOs.Pokemons.Request;
public sealed record MarkPokemonAsCaughtRequestDto
{
    public Guid PokemonId { get; set; }
    public bool IsCaught { get; set; }
}