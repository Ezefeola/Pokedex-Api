using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;

namespace Application.Contracts.Repositories;
public interface IUserPokemonRepository : IGenericRepository<UserPokemon>
{
    Task<UserPokemon?> GetUserPokemonAsync(UserId userId, PokemonId pokemonId, CancellationToken cancellationToken);
}