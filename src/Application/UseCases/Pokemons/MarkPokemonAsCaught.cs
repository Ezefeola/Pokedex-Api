using Application.Contracts.Authentication;
using Application.Contracts.Models;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Pokemons;
using Domain.Abstractions;
using Domain.Common.DomainResults;
using Domain.Pokemons.Entities;
using Domain.Pokemons.ValueObjects;
using Domain.Users.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Shared.DTOs.Pokemons.Request;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Pokemons;
public class MarkPokemonAsCaught : IMarkPokemonAsCaught
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<MarkPokemonAsCaughtRequestDto> _validator;
    private readonly IUserInfo _userInfo;
    private readonly ILogger<MarkPokemonAsCaught> _logger;

    public MarkPokemonAsCaught(
        IUnitOfWork unitOfWork,
        IValidator<MarkPokemonAsCaughtRequestDto> validator,
        IUserInfo userInfo,
        ILogger<MarkPokemonAsCaught> logger
    )
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _userInfo = userInfo;
        _logger = logger;
    }

    public async Task<Result> ExecuteAsync(
        MarkPokemonAsCaughtRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validatorResult = _validator.Validate(requestDto);
        if(!validatorResult.IsValid)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([.. validatorResult.Errors.Select(x => x.ErrorMessage)]);
        }

        UserId userId = UserId.Create(_userInfo.UserId);
        PokemonId pokemonId = PokemonId.Create(requestDto.PokemonId);

        bool pokemonExists = await _unitOfWork.PokemonRepository.ExistsByIdAsync(pokemonId, cancellationToken);
        if(!pokemonExists)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.Pokemons.NOT_FOUND]);
        }
        bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
        if (!userExists)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.Users.NOT_FOUND]);
        }

        UserPokemon? userPokemon = await _unitOfWork.UserPokemonRepository.GetUserPokemonAsync(userId, pokemonId, cancellationToken);
        if (userPokemon is not null)
        {
            _logger.LogInformation("UserPokemon exists, starting update.");
            userPokemon.MarkAsCaught(!userPokemon.IsCaught);
            _logger.LogInformation($"Tracked userPokemon IsCaught updated and marked as updated " +
                $"- IsCaught: {userPokemon.IsCaught} " +
                $"- UpdatedAt: {userPokemon.UpdatedAt}"
            );
        }
        else
        {
            DomainResult<UserPokemon> newUserPokemonResult = UserPokemon.Create(
                userId,
                pokemonId
            );
            if(!newUserPokemonResult.IsSuccess)
            {
                return Result.Failure(HttpStatusCode.BadRequest)
                             .WithErrors(newUserPokemonResult.Errors);
            }
            await _unitOfWork.UserPokemonRepository.AddAsync(newUserPokemonResult.Value, cancellationToken);
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