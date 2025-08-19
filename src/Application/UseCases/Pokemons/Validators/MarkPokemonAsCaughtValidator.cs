using Domain.Abstractions;
using FluentValidation;
using Shared.DTOs.Pokemons.Request;

namespace Application.UseCases.Pokemons.Validators;
public class MarkPokemonAsCaughtValidator : AbstractValidator<MarkPokemonAsCaughtRequestDto>
{
    public MarkPokemonAsCaughtValidator()
    {
        RuleFor(x => x.PokemonId)
            .NotEmpty()
                .WithMessage(DomainErrors.UserPokemons.POKEMON_ID_NOT_EMPTY);
    }
}