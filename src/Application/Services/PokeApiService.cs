using Application.Contracts.Services;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Application.Services;
public class PokeApiService : IPokeApiService
{
    private readonly HttpClient _httpClient;
    private const string PokeApiUrl = "https://pokeapi.co/api/v2/pokemon";
    public PokeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches all Pokémon resource URLs from the API, handling pagination automatically.
    /// </summary>
    /// <returns>A list of all Pokémon resources (name and URL).</returns>
    public async Task<List<PokemonResource>> GetAllPokemonResourcesAsync()
    {
        List<PokemonResource> allPokemonResources = [];
        string? nextUrl = $"{PokeApiUrl}?limit=200";

        while (nextUrl != null)
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>(nextUrl);
            if (response?.Results != null)
            {
                allPokemonResources.AddRange(response.Results);
            }
            nextUrl = response?.Next;
        }

        return allPokemonResources;
    }

    /// <summary>
    /// Fetches the detailed information for a single Pokémon using its URL.
    /// </summary>
    /// <param name="url">The URL for the Pokémon's API resource.</param>
    /// <returns>The detailed Pokémon data, or null if the request fails.</returns>
    public async Task<PokemonApiResponse?> GetPokemonDetailsAsync(string url)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PokemonApiResponse>(url);
        }
        catch (HttpRequestException ex)
        {
            // Log the error (e.g., Console.WriteLine, or a proper logging framework)
            Console.WriteLine($"Error fetching Pokémon details from {url}: {ex.Message}");
            return null;
        }
    }
}
// Represents the paginated list of all Pokémon resources
public class PokemonListResponse
{
    [JsonPropertyName("results")]
    public List<PokemonResource> Results { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}

public class PokemonResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

// Represents the detailed data for a single Pokémon
public class PokemonApiResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("sprites")]
    public PokemonSprites Sprites { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonTypeInfo> Types { get; set; }
}

public class PokemonSprites
{
    [JsonPropertyName("other")]
    public OtherSprites Other { get; set; }
}

public class OtherSprites
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtwork OfficialArtwork { get; set; }
}

public class OfficialArtwork
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; }
}

public class PokemonTypeInfo
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public PokemonType Type { get; set; }
}

public class PokemonType
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}