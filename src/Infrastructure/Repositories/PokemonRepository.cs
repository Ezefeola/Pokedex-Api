using Application.Contracts.Repositories;
using Application.Utilities.QueryOptions;
using Application.Utilities.QueryOptions.Pagination;
using Domain.Pokemons;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;

namespace Infrastructure.Repositories;
public class PokemonRepository : GenericRepository<Pokemon, PokemonId>, IPokemonRepository
{
    public PokemonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<PokemonResponseDto>> GetPaginatedByCriteriaAsync(
        GetPokemonsRequestDto requestDto,
        UserId userId,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Pokemon> query = Query();

        var joinedQuery = query.GroupJoin(
            _context.Set<UserPokemon>().Where(up => up.UserId == userId),
            pokemon => pokemon.Id,
            userPokemon => userPokemon.PokemonId,
            (pokemon, userPokemons) => new
            {
                Pokemon = pokemon,
                UserPokemon = userPokemons.FirstOrDefault()
            }
        );

        if (!string.IsNullOrEmpty(requestDto.Name))
        {
            joinedQuery = joinedQuery.Where(p => p.Pokemon.Name.Contains(requestDto.Name));
        }

        if (!string.IsNullOrEmpty(requestDto.Type))
        {
            joinedQuery = joinedQuery.Where(p => p.Pokemon.Type1 == requestDto.Type || p.Pokemon.Type2 == requestDto.Type);
        }

        if (requestDto.IsCaught.HasValue)
        {
            joinedQuery = joinedQuery.Where(p => (p.UserPokemon != null && p.UserPokemon.IsCaught) == requestDto.IsCaught.Value);
        }

        List<PokemonResponseDto> pokemonResponseDtos = await joinedQuery.OrderBy(x => x.Pokemon.Number)
                                                                        .ApplyPagination
                                                                        (
                                                                            requestDto.GetPageIndex(),
                                                                            requestDto.GetPageSize()
                                                                        )
                                                                        .Select(x => new PokemonResponseDto
                                                                        {
                                                                            Id = x.Pokemon.Id.Value,
                                                                            Number = x.Pokemon.Number,
                                                                            Name = x.Pokemon.Name,
                                                                            Weight = x.Pokemon.Weight,
                                                                            Height = x.Pokemon.Height,
                                                                            ImageUrl = x.Pokemon.ImageUrl,
                                                                            Type1 = x.Pokemon.Type1,
                                                                            Type2 = x.Pokemon.Type2,
                                                                            IsCaught = x.UserPokemon != null ? x.UserPokemon.IsCaught : false 
                                                                        })
                                                                        .ToListAsync(cancellationToken);
        return pokemonResponseDtos;
    }

    public async Task<int> GetHighestPokemonNumberAsync()
    {
       if(await Query().AnyAsync())
        {
            return await Query()
                         .MaxAsync(x => x.Number);
        }

        return 0;
    }
}