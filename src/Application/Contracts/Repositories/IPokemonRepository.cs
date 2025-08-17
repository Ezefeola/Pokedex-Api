using Domain.Pokemons;

namespace Application.Contracts.Repositories;
public interface IPokemonRepository : IGenericRepository<Pokemon, int>
{
    Task<int> GetHighestPokemonNumberAsync();
}