using Application.Contracts.Authentication;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Response;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Pokemons;
public class GetPokemonsCount : IGetPokemonsCount
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfo _userInfo;

    public GetPokemonsCount(
        IUnitOfWork unitOfWork,
        IUserInfo userInfo
    )
    {
        _unitOfWork = unitOfWork;
        _userInfo = userInfo;
    }

    public async Task<Result<GetPokemonsCountResponseDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        UserId userId = UserId.Create(_userInfo.UserId);
        GetPokemonsCountResponseDto pokemonsCountResponseDto = await _unitOfWork.UserPokemonRepository.GetPokemonsCountAsync(userId, cancellationToken);

        return Result<GetPokemonsCountResponseDto>.Success(HttpStatusCode.OK)
                                                  .WithPayload(pokemonsCountResponseDto);
    }
}