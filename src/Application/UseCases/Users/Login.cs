using Application.Contracts.Authentication;
using Application.Contracts.UnitOfWork;
using Application.Contracts.UseCases.Users;
using Application.Utilities.Validations;
using Domain.Users;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs.Users.Request;
using Shared.DTOs.Users.Response;
using Shared.Result;
using System.Net;

namespace Application.UseCases.Users;
public class Login : ILogin
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<LoginRequestDto> _validator;

    public Login(
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider,
        IPasswordHasher<User> passwordHasher,
        IValidator<LoginRequestDto> validator
    )
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<Result<LoginResponseDto>> ExecuteAsync(
        LoginRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validatorResult = _validator.Validate(requestDto);
        if (!validatorResult.IsValid)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([.. validatorResult.Errors.Select(e => e.ErrorMessage)]);
        }

        User? user = await _unitOfWork.UserRepository.GetByEmailAsync(requestDto.Email, cancellationToken);
        if (user is null)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([ValidationMessages.Auth.INVALID_CREDENTIALS]);
        }

        PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, requestDto.Password);
        if (passwordVerificationResult != PasswordVerificationResult.Success)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([ValidationMessages.Auth.INVALID_CREDENTIALS]);
        }

        LoginResponseDto responseDto = new()
        {
            Token = _tokenProvider.GenerateToken(user)
        };
        return Result<LoginResponseDto>.Success(HttpStatusCode.OK)
                                       .WithPayload(responseDto);
    }
}
