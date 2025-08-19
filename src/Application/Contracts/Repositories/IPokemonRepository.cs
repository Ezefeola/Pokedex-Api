using Domain.Pokemons;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;

namespace Application.Contracts.Repositories;
public interface IPokemonRepository : IGenericRepository<Pokemon, PokemonId>
{
    Task<int> GetHighestPokemonNumberAsync();
    Task<List<PokemonResponseDto>> GetPaginatedByCriteriaAsync(GetPokemonsRequestDto requestDto, UserId userId, CancellationToken cancellationToken);
}