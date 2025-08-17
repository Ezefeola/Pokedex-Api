using Application.Contracts.Repositories;
using Domain.Pokemons;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class PokemonRepository : GenericRepository<Pokemon, int>, IPokemonRepository
{
    public PokemonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<int> GetHighestPokemonNumberAsync()
    {
        return await Query()
                     .MaxAsync(x => x.Id);
    }
}