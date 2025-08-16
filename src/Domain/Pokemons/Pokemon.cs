using Domain.Abstractions;
using Domain.Common.DomainResults;

namespace Domain.Pokemons;
public class Pokemon : Entity<int>
{
    public static class Rules
    {
        public const int NAME_MAX_LENGTH = 456;
        public const int NAME_MIN_LENGTH = 1;

        public const int TYPE1_MAX_LENGTH = 255;

        public const int TYPE2_MAX_LENGTH = 255;
    }

    private Pokemon() { }
    public string Name { get; set; } = default!;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string ImageUrl { get; set; } = default!;
    public string Type1 { get; set; } = default!;
    public string? Type2 { get; set; }

    public static DomainResult<Pokemon> Create(
        int id,
        string name,
        decimal height,
        decimal weight,
        string imageUrl,
        string type1,
        string? type2
    )
    {
        Pokemon pokemon = new()
        {
            Id = id,
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