using Application.Contracts.UseCases.Pokemons;
using Hangfire;

namespace Infrastructure.HangFire;
public static class JobsInitializer
{
    public static void InitializeRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IUpdatePokemonsDatabase>(
            "PokemonsDatabaseUpdate",
            useCase => useCase.ExecuteAsync(CancellationToken.None),
            "0 3 * * *"
        ); 
    }
}