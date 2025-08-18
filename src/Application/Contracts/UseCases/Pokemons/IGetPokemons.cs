using Shared.DTOs.Pokemons.Request;
using Shared.DTOs.Pokemons.Response;
using Shared.Result;

namespace Application.Contracts.UseCases.Pokemons
{
    public interface IGetPokemons
    {
        Task<Result<GetPokemonsResponseDto>> ExecuteAsync(GetPokemonsRequestDto requestDto, CancellationToken cancellationToken);
    }
}