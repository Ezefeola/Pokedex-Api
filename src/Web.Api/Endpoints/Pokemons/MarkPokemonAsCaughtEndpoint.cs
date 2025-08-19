using Application.Contracts.UseCases.Pokemons;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;
using Shared.DTOs.Pokemons.Request;
using Shared.Result;

namespace Web.Api.Endpoints.Pokemons;
public class MarkPokemonAsCaughtEndpoint : IEndpoint<PokemonsEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPatch("/", MarkPokemonAsCaughtHandler)
                  .WithName("MarkPokemonAsCaught")
                  .Produces<Result>(StatusCodes.Status200OK)
                  .Produces<Result>(StatusCodes.Status404NotFound)
                  .WithSummary("Mark Pokemon As Caught")
                  .WithDescription("Mark Pokemon As Caught")
                  .RequireAuthorization();
    }

    private static async Task<Result> MarkPokemonAsCaughtHandler(
        [FromBody] MarkPokemonAsCaughtRequestDto requestDto,
        [FromServices] IMarkPokemonAsCaught useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}