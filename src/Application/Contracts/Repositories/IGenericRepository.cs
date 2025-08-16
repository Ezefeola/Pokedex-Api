using Domain.Abstractions.Interfaces;

namespace Application.Contracts.Repositories;
public interface IGenericRepository<TEntity>
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    IQueryable<TEntity> Query();
}

public interface IGenericRepository<TEntity, TId> : IGenericRepository<TEntity>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(TId id, CancellationToken cancellationToken);
}