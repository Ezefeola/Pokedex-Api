using Application.Contracts.Services;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Domain.Common.DomainResults;
using Domain.Pokemons;
using Microsoft.Extensions.Logging;
using Shared.DTOs.PokeApi.Response;

namespace Application.UseCases.Pokemons;
public class UpdatePokemonsDatabase : IUpdatePokemonsDatabase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPokeApiService _pokeApiService;
    private readonly ILogger<UpdatePokemonsDatabase> _logger;

    public UpdatePokemonsDatabase(
        IUnitOfWork unitOfWork,
        IPokeApiService pokeApiService,
        ILogger<UpdatePokemonsDatabase> logger
    )
    {
        _unitOfWork = unitOfWork;
        _pokeApiService = pokeApiService;
        _logger = logger;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting incremental Pokémon database update...");

        int highestPokemonNumberInDb = await _unitOfWork.PokemonRepository.GetHighestPokemonNumberAsync();
        _logger.LogInformation($"The last pokemon by number found in the database is: {highestPokemonNumberInDb}");

        string? nextUrl = $"https://pokeapi.co/api/v2/pokemon?offset={highestPokemonNumberInDb}&limit=200";
        int totalAdded = 0;

        while (nextUrl != null)
        {
            PokemonListResponseDto? pokemonListResponse = await _pokeApiService.GetPokemonResourcesWithUrlAsync(nextUrl);

            if (pokemonListResponse?.Results == null || pokemonListResponse.Results.Count == 0)
            {
                break;
            }

            List<Task<PokemonApiResponseDto?>> tasks = pokemonListResponse.Results
                                                        .Select(resource => _pokeApiService.GetPokemonDetailsAsync(resource.Url))
                                                        .ToList();

            PokemonApiResponseDto?[] pokemonDetails = await Task.WhenAll(tasks);

            List<Pokemon> pokemonToInsert = pokemonDetails.Where(x => x != null)
                                                          .Select(x => MapToPokemonEntity(x!))
                                                          .ToList();

            if (pokemonToInsert.Count > 0)
            {
                await _unitOfWork.PokemonRepository.AddRangeAsync(pokemonToInsert, cancellationToken);
                await _unitOfWork.CompleteAsync(cancellationToken);
                totalAdded += pokemonToInsert.Count;
                _logger.LogInformation($"Successfully inserted a batch of {pokemonToInsert.Count} new Pokémon.");
            }

            nextUrl = pokemonListResponse.Next;
        }

        _logger.LogInformation($"Update complete. Total new Pokémon added: {totalAdded}.");
    }

    private static Pokemon MapToPokemonEntity(PokemonApiResponseDto apiResponse)
    {
        try
        {
            List<PokemonTypeInfo> types = [.. apiResponse.Types.OrderBy(t => t.Slot)];
            DomainResult<Pokemon> pokemonResult = Pokemon.Create(
                apiResponse.Id,
                char.ToUpper(apiResponse.Name[0]) + apiResponse.Name[1..],
                apiResponse.Height,
                apiResponse.Weight,
                apiResponse.Sprites.Other.OfficialArtwork.FrontDefault ?? "NOIMAGE",
                types.FirstOrDefault()?.Type.Name ?? "unknown",
                types.Count > 1 ? types[1].Type.Name : null
            );
            return pokemonResult.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}