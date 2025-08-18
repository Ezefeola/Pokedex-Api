using Application.Contracts.Authentication;
using Application.Contracts.Models;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Users;
using Domain.Common.DomainResults;
using Domain.Users;
using Shared.DTOs.Users.Request;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Users;
public class Register : IRegister
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public Register(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher
    )
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> ExecuteAsync(
        RegisterRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        string passwordHash = _passwordHasher.Hash(requestDto.Password);
        DomainResult<User> userResult = User.Create(
            requestDto.FirstName,
            requestDto.LastName,
            requestDto.EmailAddress,
            passwordHash,
            User.UserRolesEnum.Admin
        );

        await _unitOfWork.UserRepository.AddAsync(userResult.Value, cancellationToken);
        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.Created);
    }
}