using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Repository.Pattern.Entities;
using Repository.Pattern.Entities.Contexts;
using Repository.Pattern.Repositories.Interfaces;

namespace Repository.Pattern.Repositories;

public sealed class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly RepositoryContext _dbContext;

    public EntityFrameworkRepository(RepositoryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>();

        var entityEntry = await dbSet.AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity;
    }

    public async Task DeleteAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await Task.Run(async () =>
        {
            var dbSet = _dbContext.Set<TEntity>();

            dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
        bool descending = false,
        Expression<Func<TEntity, dynamic>>? orderBy = default,
        IEnumerable<Expression<Func<TEntity, dynamic?>>>? includes = default,
        CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>().Where(predicate);

        if (orderBy is not null)
        {
            dbSet = descending ? dbSet.OrderByDescending(orderBy) : dbSet.OrderBy(orderBy);
        }

        if (includes is not null)
        {
            dbSet = includes.Aggregate(dbSet, (current,
                include) => current.Include(include));
        }

        return await dbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        bool descending = false,
        Expression<Func<TEntity, dynamic>>? orderBy = default,
        IEnumerable<Expression<Func<TEntity, dynamic?>>>? includes = default,
        CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>().Where(predicate);

        if (orderBy is not null)
        {
            dbSet = descending ? dbSet.OrderByDescending(orderBy) : dbSet.OrderBy(orderBy);
        }

        if (includes is not null)
        {
            dbSet = includes.Aggregate(dbSet, (current,
                include) => current.Include(include));
        }

        return await dbSet.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(async () =>
        {
            var dbSet = _dbContext.Set<TEntity>();

            var entityEntry = dbSet.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entityEntry.Entity;
        }, cancellationToken);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>();

        var entities = await dbSet.Where(predicate).ToListAsync(cancellationToken);

        await Task.Run(() => dbSet.RemoveRange(entities), cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
