using Application.Contracts.UseCases.Pokemons;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;
using Shared.DTOs.Pokemons.Response;
using Shared.Result;

namespace Web.Api.Endpoints.Pokemons;
public class GetPokemonsCountEndpoint : IEndpoint<PokemonsEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/pokemons-count", GetPokemonsCountHandler)
                  .WithName("GetPokemonsCount")
                  .Produces<Result<GetPokemonsCountResponseDto>>(StatusCodes.Status200OK)
                  .Produces<Result<GetPokemonsCountResponseDto>>(StatusCodes.Status404NotFound)
                  .WithSummary("Get Pokemons Count")
                  .WithDescription("Get Pokemons Count")
                  .RequireAuthorization();
    }

    private static async Task<Result<GetPokemonsCountResponseDto>> GetPokemonsCountHandler(
        [FromServices] IGetPokemonsCount useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetPokemonsCountResponseDto> response = await useCase.ExecuteAsync(cancellationToken);
        return response;
    }
}