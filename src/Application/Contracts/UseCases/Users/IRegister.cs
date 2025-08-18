using Shared.DTOs.Users.Request;
using Shared.Result;

namespace Application.Contracts.UseCases.Users
{
    public interface IRegister
    {
        Task<Result> ExecuteAsync(RegisterRequestDto requestDto, CancellationToken cancellationToken);
    }
}