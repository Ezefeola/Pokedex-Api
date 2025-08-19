using Application.Contracts.Authentication;
using Application.Contracts.Models;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using Shared.DTOs.Pokemons.Request;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Pokemons;
public class MarkPokemonAsCaught : IMarkPokemonAsCaught
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
        if (userPokemon is not null)
        {
            userPokemon.MarkAsCaught(requestDto.IsCaught);
        }
        else
        {
            UserPokemon newUserPokemon = UserPokemon.Create(
                userId,
                pokemonId
            );
            await _unitOfWork.UserPokemonRepository.AddAsync(newUserPokemon, cancellationToken);
        }

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.Conflict)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.OK);
    }
}