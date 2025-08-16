using Shared.DTOs.Users.Request;
using Shared.DTOs.Users.Response;
using Shared.Result;

namespace Application.Contracts.UseCases.Users;
public interface ILogin
{
    Task<Result<LoginResponseDto>> ExecuteAsync(LoginRequestDto requestDto, CancellationToken cancellationToken);
}