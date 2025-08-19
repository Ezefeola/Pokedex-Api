using Shared.DTOs.Pokemons.Response;
using Shared.Result;

namespace Application.Contracts.UseCases.Pokemons
{
    public interface IGetPokemonsCount
    {
        Task<Result<GetPokemonsCountResponseDto>> ExecuteAsync(CancellationToken cancellationToken);
    }
}