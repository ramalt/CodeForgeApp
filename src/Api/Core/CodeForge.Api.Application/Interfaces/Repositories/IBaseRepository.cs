using System.Linq.Expressions;
using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    Task AddAsync(IEnumerable<TEntity> entities);
    void AddOrUpdate(TEntity entity);
    Task AddOrUpdateAsync(TEntity entity);

    IQueryable<TEntity> AsQueryable();

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetAll(bool noTracking = true);
    Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);

    Task BulkAdd(IEnumerable<TEntity> entities);
    void BulkDelete(Expression<Func<TEntity, bool>> predicate);
    void BulkDelete(IEnumerable<TEntity> entities);
    void BulkDeleteById(IEnumerable<Guid> ids);
    void BulkUpdate(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);
    void Delete(Guid id);
    void DeleteRange(Expression<Func<TEntity, bool>> predicate);
    void DeleteRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity);
}
