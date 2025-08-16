using Application.Contracts.Repositories;
using Domain.Abstractions.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
}

public class GenericRepository<TEntity, TId> : GenericRepository<TEntity>, IGenericRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public GenericRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> SoftDeleteAsync(TId id, CancellationToken cancellationToken)
    {
        TEntity? entity = await GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        entity.SoftDelete();

        return true;
    }
}