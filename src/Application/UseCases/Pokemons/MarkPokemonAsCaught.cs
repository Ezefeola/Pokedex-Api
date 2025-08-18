using Application.Contracts.Authentication;
using Application.Contracts.UnitOfWork;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Request;
using Shared.Result;

namespace Application.UseCases.Pokemons;
public class MarkPokemonAsCaught
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfo _userInfo;

    public MarkPokemonAsCaught(
        IUnitOfWork unitOfWork,
        IUserInfo userInfo
    )
    {
        _unitOfWork = unitOfWork;
        _userInfo = userInfo;
    }

    public async Task<Result> ExecuteAsync(
        MarkPokemonAsCaughtRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        UserId userId = UserId.Create(_userInfo.UserId);
        PokemonId pokemonId = PokemonId.Create(requestDto.PokemonId);

        UserPokemon? userPokemon = await _unitOfWork.UserPokemonRepository.GetUserPokemonAsync(userId, pokemonId, cancellationToken);

    }
}