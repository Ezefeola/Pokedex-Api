using Application.Utilities.Validations;
using FluentValidation;
using Shared.DTOs.Users.Request;

namespace Application.UseCases.Users.Validators;
public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .EmailAddress()
                .WithMessage(ValidationMessages.INVALID_EMAIL);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);
    }
}