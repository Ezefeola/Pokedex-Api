using Application.Contracts.Models;
using Application.Contracts.Repositories;

namespace Application.Contracts.UnitOfWork;
public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IPokemonRepository PokemonRepository { get; }
    public Task<SaveResult> CompleteAsync(CancellationToken cancellationToken = default);
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
