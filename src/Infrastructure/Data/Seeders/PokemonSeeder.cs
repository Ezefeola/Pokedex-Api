//using Application.Contracts.Services;
//using Application.Services;
//using Domain.Common.DomainResults;
//using Domain.Pokemons;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace Infrastructure.Data.Seeders;
//public static class PokemonSeeder
//{

//    /// <summary>
//    /// Seeds the database with Pokémon from the PokéAPI.
//    /// It fetches only the Pokémon that are not already in the database.
//    /// </summary>
//    /// <param name="batchSize">The number of Pokémon to process and insert at a time.</param>
//    public static async Task Seed(
//        IPokeApiService pokeApiService, 
//        ILogger logger,
//        DbContext dbContext
//    )
//    {
//        logger.LogInformation("Starting Pokémon database seed...");

//        // Step 1: Get IDs of Pokémon already in our database to avoid duplicates.
//        HashSet<int> existingPokemonIds = await dbContext.Set<Pokemon>()
//                                                         .Select(p => p.Id)
//                                                         .ToHashSetAsync();
//        logger.LogInformation($"Found {existingPokemonIds.Count} existing Pokémon in the database.");

//        // Step 2: Get all Pokémon resources from the API.
//        List<PokemonResource> allPokemonResources = await pokeApiService.GetAllPokemonResourcesAsync();
//        logger.LogInformation($"Fetched {allPokemonResources.Count} total Pokémon resources from PokéAPI.");

//        // Step 3: Filter out the ones we already have.
//        List<PokemonResource> newPokemonResources = [.. allPokemonResources.Where(resource =>
//        {
//            // The ID is in the URL, e.g., ".../pokemon/1/"
//            string idString = resource.Url.TrimEnd('/').Split('/').Last();
//            return int.TryParse(idString, out int id) && !existingPokemonIds.Contains(id);
//        })];

//        logger.LogInformation($"Found {newPokemonResources.Count} new Pokémon to add.");

//        if (newPokemonResources.Count == 0)
//        {
//            logger.LogInformation("Database is already up-to-date.");
//            return;
//        }

//        int batchSizeToProcessPokemons = 200;
//        // Step 4: Process and insert the new Pokémon in batches.
//        int totalAdded = 0;
//        for (int i = 0; i < newPokemonResources.Count; i += batchSizeToProcessPokemons)
//        {
//            List<PokemonResource> batchResources = [.. newPokemonResources.Skip(i).Take(batchSizeToProcessPokemons)];
//            logger.LogInformation($"Processing batch {i / batchSizeToProcessPokemons + 1} with {batchResources.Count} Pokémon...");

//            List<Task<PokemonApiResponse?>> tasks = batchResources.Select
//                                                                    (
//                                                                        resource =>
//                                                                        pokeApiService.GetPokemonDetailsAsync(resource.Url)
//                                                                    )
//                                                                   .ToList();
//            PokemonApiResponse?[] pokemonDetails = await Task.WhenAll(tasks);

//            List<Pokemon> pokemonToInsert = pokemonDetails.Where(x => x is not null)
//                                                          .Select(x => MapToPokemonEntity(x))
//                                                          .ToList();

//            if (pokemonToInsert.Count > 0)
//            {
//                await dbContext.Set<Pokemon>().AddRangeAsync(pokemonToInsert);
//                await dbContext.SaveChangesAsync();
//                totalAdded += pokemonToInsert.Count;
//                logger.LogInformation($"Successfully inserted {pokemonToInsert.Count} Pokémon into the database.");
//            }
//        }

//        logger.LogInformation($"Seeding complete. Total new Pokémon added: {totalAdded}.");
//    }


//    /// <summary>
//    /// Maps the API response object to our database entity object.
//    /// </summary>
//    private static Pokemon MapToPokemonEntity(PokemonApiResponse apiResponse)
//    {
//        try
//        {
//            List<PokemonTypeInfo> types = [.. apiResponse.Types.OrderBy(t => t.Slot)];
//            DomainResult<Pokemon> pokemonResult = Pokemon.Create(
//                apiResponse.Id,
//                char.ToUpper(apiResponse.Name[0]) + apiResponse.Name.Substring(1),
//                apiResponse.Height,
//                apiResponse.Weight,
//                apiResponse.Sprites.Other.OfficialArtwork.FrontDefault ?? "NOIMAGE",
//                types.FirstOrDefault()?.Type.Name ?? "unknown",
//                types.Count > 1 ? types[1].Type.Name : null
//            );
//            return pokemonResult.Value;
//        }
//        catch (Exception)
//        {
//            throw;
//        }

//    }
//}