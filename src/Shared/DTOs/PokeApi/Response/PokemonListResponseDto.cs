using System.Text.Json.Serialization;

namespace Shared.DTOs.PokeApi.Response;
public class PokemonListResponseDto
{
    [JsonPropertyName("results")]
    public List<PokemonResource> Results { get; set; } = [];

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}

public class PokemonResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}