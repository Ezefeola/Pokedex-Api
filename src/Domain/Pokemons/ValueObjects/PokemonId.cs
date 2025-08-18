using Domain.Abstractions.StronglyTypedIds;

namespace Domain.Pokemons.ValueObjects;
public sealed record PokemonId : StronglyTypedGuidId<PokemonId>
{

}