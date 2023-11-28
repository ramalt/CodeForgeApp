using System.Linq.Expressions;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Api.Domain.Models.Abstracts;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    private readonly DbContext _context;
    private DbSet<TEntity> _entity => _context.Set<TEntity>();
    protected BaseRepository(DbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    // INSERT
    public virtual void Add(TEntity entity)
    {
        _entity.Add(entity);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
    }

    public virtual void Add(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
            _entity.AddRange(entities);
    }

    public virtual async Task AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
            await _entity.AddRangeAsync(entities);
    }

    public virtual void AddOrUpdate(TEntity entity)
    {
        if (!_entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            _context.Update(entity);
    }

    public virtual async Task AddOrUpdateAsync(TEntity entity)
    {
        if (!_entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            _context.Update(entity);
    }

    // READ
    public virtual IQueryable<TEntity> AsQueriable() => _entity.AsQueryable();

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetQueryWithIncludes(includes, noTracking);
        return await query.FirstOrDefaultAsync(predicate);
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetQueryWithIncludes(includes, noTracking);

        if (predicate != null)
            query = query.Where(predicate);

        return query;
    }

    public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
    {
        var query = noTracking ? _entity.AsNoTracking() : _entity;
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetQueryWithIncludes(includes, noTracking);
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetQueryWithIncludes(includes, noTracking);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            query = orderBy(query);

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetQueryWithIncludes(includes, noTracking);

        if (predicate != null)
            query = query.Where(predicate);

        return await query.SingleOrDefaultAsync();
    }

    // BULK
    public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
            await _entity.AddRangeAsync(entities);
    }

    public virtual void BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(_entity.Where(predicate));
    }

    public virtual void BulkDelete(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
            _entity.RemoveRange(entities);
    }

    public virtual void BulkDeleteById(IEnumerable<Guid> ids)
    {
        if (ids != null && ids.Any())
            _context.RemoveRange(_entity.Where(i => ids.Contains(i.Id)));
    }

    public virtual void BulkUpdate(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
        {
            foreach (var entityItem in entities)
            {
                _entity.Update(entityItem);
            }
        }
    }

    // DELETE
    public virtual void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _entity.Attach(entity);

        _entity.Remove(entity);
    }

    public virtual void Delete(Guid id)
    {
        var entity = _entity.Find(id);
        Delete(entity);
    }

    public virtual void DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(_entity.Where(predicate));
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
            _entity.RemoveRange(entities);
    }

    // UPDATE
    public virtual void Update(TEntity entity)
    {
        _entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<TEntity> GetQueryWithIncludes(Expression<Func<TEntity, object>>[] includes, bool noTracking)
    {
        var query = _entity.AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (noTracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<TEntity> AsQueryable() => _entity.AsQueryable();
}
