using Application.Services;
using Shared.DTOs.PokeApi.Response;

namespace Application.Contracts.Services
{
    public interface IPokeApiService
    {
        Task<PokemonApiResponseDto?> GetPokemonDetailsAsync(string url);
        Task<PokemonListResponseDto?> GetPokemonResourcesWithUrlAsync(string url);
    }
}