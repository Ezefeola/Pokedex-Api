using Application.Utilities.QueryOptions.Pagination;
using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;

namespace Application.Utilities.Mappers;
public static class PokemonMappers
{
    public static GetPokemonsResponseDto ToGetPokemonsResponseDto(
        this List<PokemonResponseDto> pokemonResponseDtos, 
        GetPokemonsRequestDto requestDto,
        int totalPokemonsCount
    )
    {
        return new GetPokemonsResponseDto()
        {
            PageIndex = requestDto.GetPageIndex(),
            PageSize = requestDto.GetPageSize(),
            TotalPages = requestDto.GetTotalPages(totalPokemonsCount),
            TotalRecords = totalPokemonsCount,
            Items = pokemonResponseDtos
        };
    }
}