using Application.Contracts.UseCases.Users;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;
using Shared.DTOs.Users.Request;
using Shared.DTOs.Users.Response;
using Shared.Result;

namespace Web.Api.Endpoints.Auth;
public class LoginEndpoint : IEndpoint<AuthEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/", LoginHandler)
                  .WithName("Login")
                  .Produces<Result<LoginResponseDto>>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status401Unauthorized)
                  .WithSummary("Login")
                  .WithDescription("Login");
    }

    private static async Task<Result<LoginResponseDto>> LoginHandler(
        [FromBody] LoginRequestDto requestDto,
        [FromServices] ILogin useCase,
        CancellationToken cancellationToken
    )
    {
        Result<LoginResponseDto> response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}