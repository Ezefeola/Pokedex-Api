using Application.Services;

namespace Application.Contracts.Services
{
    public interface IPokeApiService
    {
        Task<List<PokemonResource>> GetAllPokemonResourcesAsync();
        Task<PokemonApiResponse?> GetPokemonDetailsAsync(string url);
    }
}