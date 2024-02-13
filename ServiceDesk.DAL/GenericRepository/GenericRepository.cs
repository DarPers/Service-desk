﻿using Microsoft.EntityFrameworkCore;
using ServiceDesk.DAL.Data;
using System.Linq.Expressions;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.DAL.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;

    private readonly DbSet<TEntity> _entities;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public async Task AddEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _entities.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetEntityByPredicateAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync(predicate, cancellationToken);
    }

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }

    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.AsEnumerable().Where(predicate).ToList();
    }

    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _entities;
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}