using Domain.Abstractions;
using Domain.Common.DomainResults;
using Domain.Pokemons.ValueObjects;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Domain.Pokemons.Entities;
public sealed class UserPokemon : Entity
{
    private UserPokemon() { }

    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public PokemonId PokemonId { get; private set; } = default!;
    public Pokemon Pokemon { get; private set; } = default!;
    public bool IsCaught { get; private set; }

    public static DomainResult<UserPokemon> Create(
        UserId userId,
        PokemonId pokemonId
    )
    {
        UserPokemon userPokemon = new()
        {
            UserId = userId,
            PokemonId = pokemonId,
            IsCaught = true
        };
        return DomainResult<UserPokemon>.Success(userPokemon);
    }

    public void MarkAsCaught(bool isCaught)
    {
        IsCaught = isCaught;
        UpdatedAt = DateTime.UtcNow;
    }
}