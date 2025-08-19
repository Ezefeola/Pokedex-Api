using Application.Contracts.Repositories;
using Domain.Pokemons;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Pokemons.Response;

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

    public async Task<GetPokemonsCountResponseDto> GetPokemonsCountAsync(UserId userId, CancellationToken cancellationToken)
    {
        int totalPokemonCount = await _context.Set<Pokemon>().CountAsync(cancellationToken);

        int caughtCount = await Query()
                                .Where(up => up.UserId == userId && up.IsCaught)
                                .CountAsync(cancellationToken);

        int uncaughtCount = totalPokemonCount - caughtCount;

        return new GetPokemonsCountResponseDto
        {
            CaughtCount = caughtCount,
            UncaughtCount = uncaughtCount
        };
    }
}