using Application.Contracts.Repositories;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class UserPokemonRepository : GenericRepository<UserPokemon>, IUserPokemonRepository
{
    public UserPokemonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserPokemon?> GetUserPokemonAsync(
        UserId userId,
        PokemonId pokemonId,
        CancellationToken cancellationToken
    )
    {
        return await Query()
                     .Where(x => x.UserId == userId &&
                            x.PokemonId == pokemonId
                     )
                     .FirstOrDefaultAsync(cancellationToken);
    }
}