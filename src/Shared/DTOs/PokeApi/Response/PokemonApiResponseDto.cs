using System.Text.Json.Serialization;

namespace Shared.DTOs.PokeApi.Response;
public class PokemonApiResponseDto
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
    public PokemonSprites Sprites { get; set; } = default!;

    [JsonPropertyName("types")]
    public List<PokemonTypeInfo> Types { get; set; } = [];
}

public class PokemonSprites
{
    [JsonPropertyName("other")]
    public OtherSprites Other { get; set; } = default!;
}

public class OtherSprites
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtwork OfficialArtwork { get; set; } = default!;
}

public class OfficialArtwork
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = default!;
}

public class PokemonTypeInfo
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public PokemonType Type { get; set; } = default!;
}

public class PokemonType
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}