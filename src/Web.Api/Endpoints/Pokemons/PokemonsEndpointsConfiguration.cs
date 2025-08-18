using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Web.Api.Endpoints.Pokemons;
public class PokemonsEndpointsConfiguration : EndpointsConfiguration
{
    public PokemonsEndpointsConfiguration()
    {
        WithPrefix("pokemons");
        WithTags("pokemons");
    }
}