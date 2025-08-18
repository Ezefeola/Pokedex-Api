using Application.Contracts.UseCases.Users;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;
using Shared.DTOs.Users.Request;
using Shared.Result;

namespace Web.Api.Endpoints.Auth;
public class RegisterEndpoint : IEndpoint<AuthEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/register", RegisterHandler)
                  .WithName("Register")
                  .Produces<Result>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .WithSummary("Register")
                  .WithDescription("Register"); ;
    }

    private static async Task<Result> RegisterHandler(
        [FromBody] RegisterRequestDto requestDto,
        [FromServices] IRegister useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}