using Shared.DTOs.Common;

namespace Shared.DTOs.Pokemons.Response;
public sealed record GetPokemonsResponseDto : PaginatedResponseDto<PokemonResponseDto>
{
}