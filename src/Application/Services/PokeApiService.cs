using Application.Contracts.Services;
using Shared.DTOs.PokeApi.Response;
using System.Net.Http.Json;

namespace Application.Services;
public class PokeApiService : IPokeApiService
{
    private readonly HttpClient _httpClient;
    private const string PokeApiBaseUrl = "https://pokeapi.co/api/v2/pokemon";

    public PokeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches a paginated list of Pokémon resources from the API using a specified URL.
    /// This is a generic method used by other parts of the application.
    /// </summary>
    /// <param name="url">The API URL to fetch from (can include offset and limit).</param>
    /// <returns>A paginated list of Pokémon resources.</returns>
    public async Task<PokemonListResponseDto?> GetPokemonResourcesWithUrlAsync(string url)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PokemonListResponseDto>(url);
        }
        catch (HttpRequestException ex)
        {
            // You can replace this with a proper logging framework in a real application
            Console.WriteLine($"Error fetching Pokémon list from {url}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Fetches the detailed information for a single Pokémon using its URL.
    /// </summary>
    /// <param name="url">The URL for the Pokémon's API resource.</param>
    /// <returns>The detailed Pokémon data, or null if the request fails.</returns>
    public async Task<PokemonApiResponseDto?> GetPokemonDetailsAsync(string url)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PokemonApiResponseDto>(url);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching Pokémon details from {url}: {ex.Message}");
            return null;
        }
    }
}