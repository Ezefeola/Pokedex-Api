using Application.Contracts.Authentication;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Application.Utilities.Mappers;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Pokemons;
public class GetPokemons : IGetPokemons
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfo _userInfo;

    public GetPokemons(
        IUnitOfWork unitOfWork,
        IUserInfo userInfo
    )
    {
        _unitOfWork = unitOfWork;
        _userInfo = userInfo;
    }

    public async Task<Result<GetPokemonsResponseDto>> ExecuteAsync(
        GetPokemonsRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        UserId userId = UserId.Create(_userInfo.UserId);
        List<PokemonResponseDto> pokemonResponseDtos = await _unitOfWork.PokemonRepository.GetPaginatedByCriteriaAsync(
            requestDto,
            userId,
            cancellationToken
        );

        int totalPokemonsCount = await _unitOfWork.PokemonRepository.CountAsync(cancellationToken);

        GetPokemonsResponseDto responseDto = pokemonResponseDtos.ToGetPokemonsResponseDto(requestDto, totalPokemonsCount);
        return Result<GetPokemonsResponseDto>.Success(HttpStatusCode.OK)
                                             .WithPayload(responseDto);
    }
}