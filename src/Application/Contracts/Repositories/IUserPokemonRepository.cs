using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Response;

namespace Application.Contracts.Repositories;
public interface IUserPokemonRepository : IGenericRepository<UserPokemon>
{
    Task<GetPokemonsCountResponseDto> GetPokemonsCountAsync(UserId userId, CancellationToken cancellationToken);
    Task<UserPokemon?> GetUserPokemonAsync(UserId userId, PokemonId pokemonId, CancellationToken cancellationToken);
}