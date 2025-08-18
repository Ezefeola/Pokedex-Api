using Application.Contracts.UseCases.Pokemons;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;
using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;
using Shared.Result;

namespace Web.Api.Endpoints.Pokemons;
public class GetPokemonsEndpoint : IEndpoint<PokemonsEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/", GetPokemonsHandler)
                  .WithName("GetPokemons")
                  .Produces<Result<GetPokemonsResponseDto>>(StatusCodes.Status200OK)
                  .WithSummary("Get Pokemons")
                  .WithDescription("Get Pokemons")
                  .RequireAuthorization();
    }

    private static async Task<Result<GetPokemonsResponseDto>> GetPokemonsHandler(
        [AsParameters] GetPokemonsRequestDto requestDto,
        [FromServices] IGetPokemons useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetPokemonsResponseDto> response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}