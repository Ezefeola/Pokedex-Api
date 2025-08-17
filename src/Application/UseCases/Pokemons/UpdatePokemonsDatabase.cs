using Application.Contracts.Services;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Domain.Pokemons;
using Shared.DTOs.PokeApi.Response;

namespace Application.UseCases.Pokemons;
public class UpdatePokemonsDatabase : IUpdatePokemonsDatabase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPokeApiService _pokeApiService;

    public UpdatePokemonsDatabase(
        IUnitOfWork unitOfWork,
        IPokeApiService pokeApiService
    )
    {
        _unitOfWork = unitOfWork;
        _pokeApiService = pokeApiService;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting incremental Pokémon database update...");

        //int startingId = 1;
        int highestPokemonNumberInDb = await _unitOfWork.PokemonRepository.GetHighestPokemonNumberAsync();

        string? nextUrl = $"https://pokeapi.co/api/v2/pokemon?offset={highestPokemonNumberInDb - 1}&limit=200";
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
                                                          .Select(MapToPokemonEntity)
                                                          .ToList();

            if (pokemonToInsert.Count > 0)
            {
                await _unitOfWork.PokemonRepository.AddRangeAsync(pokemonToInsert, cancellationToken);
                totalAdded += pokemonToInsert.Count;
                Console.WriteLine($"Successfully inserted a batch of {pokemonToInsert.Count} new Pokémon.");
            }

            nextUrl = pokemonListResponse.Next;
        }

        Console.WriteLine($"Update complete. Total new Pokémon added: {totalAdded}.");
    }

    private Pokemon MapToPokemonEntity(PokemonApiResponseDto apiResponse)
    {
        // Your mapping logic here
        // This can also be moved to a separate static class or a Mapper service
        throw new NotImplementedException();
    }
}