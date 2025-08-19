using Domain.Abstractions;
using Domain.Common.DomainResults;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;

namespace Domain.Pokemons;
public class Pokemon : Entity<PokemonId>
{
    public static class Rules
    {
        public const int NAME_MAX_LENGTH = 456;
        public const int NAME_MIN_LENGTH = 1;

        public const int TYPE1_MAX_LENGTH = 255;

        public const int TYPE2_MAX_LENGTH = 255;
    }

    private Pokemon() { }

    public int Number { get; set; }
    public string Name { get; private set; } = default!;
    public decimal Height { get; private set; }
    public decimal Weight { get; private set; }
    public string ImageUrl { get; private set; } = default!;
    public string Type1 { get; private set; } = default!;
    public string? Type2 { get; private set; }
    private readonly List<UserPokemon> _userPokemons = [];
    public IReadOnlyList<UserPokemon> UserPokemons => _userPokemons;

    public static DomainResult<Pokemon> Create(
        int number,
        string name,
        decimal height,
        decimal weight,
        string imageUrl,
        string type1,
        string? type2
    )
    {
        List<string> errors = [];

        if (string.IsNullOrEmpty(name)) errors.Add(DomainErrors.Pokemons.NAME_NOT_EMPTY);

        Pokemon pokemon = new()
        {
            Id = PokemonId.NewId(),
            Number = number,
            Name = name,
            Height = height,
            Weight = weight,
            ImageUrl = imageUrl,
            Type1 = type1,
            Type2 = type2
        };
        return DomainResult<Pokemon>.Success(pokemon);
    }
}