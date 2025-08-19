using Shared.DTOs.Pokemons.Request;
using Shared.Result;

namespace Application.Contracts.UseCases.Pokemons
{
    public interface IMarkPokemonAsCaught
    {
        Task<Result> ExecuteAsync(MarkPokemonAsCaughtRequestDto requestDto, CancellationToken cancellationToken);
    }
}