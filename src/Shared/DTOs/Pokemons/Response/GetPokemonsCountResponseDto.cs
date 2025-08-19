namespace Shared.DTOs.Pokemons.Response;
public sealed record GetPokemonsCountResponseDto
{
    public int CaughtCount { get; set; }
    public int UncaughtCount { get; set; }
}