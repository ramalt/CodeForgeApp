using System.Linq.Expressions;
using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity: Entity
{
    //ADD
    int Add(TEntity entity);
    Task<int> AddAsync(TEntity entity);
    int Add(IEnumerable<TEntity> entities);
    Task<int> AddAsync(IEnumerable<TEntity> entity);
    int AddOrUpdate(TEntity entity);
    Task<int> AddOrUpdateAsycnc(TEntity entity);

    // UPDATE
    Task<int> UpdateAsync(TEntity entity);
    int Update(TEntity entity);
    
    //DELETE
    int Delete(TEntity entity);
    int Delete(Guid id);
    Task<int> DeleteAsync(TEntity entity);
    Task<int> DeleteAsync(Guid id);
    bool DeleteRange(Expression<Func<TEntity, bool>> predicate);
    Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

    //GET
    IQueryable<TEntity> AsQueriable();
    Task<List<TEntity>> GetAll(bool notTracking = true);
    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool notTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetByIdAsync(Guid id, bool notTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,bool id, bool notTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,bool id, bool notTracking = true, params Expression<Func<TEntity, object>>[] includes);
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate,bool id, bool notTracking = true, params Expression<Func<TEntity, object>>[] includes);

    //BULK
    Task BulkDeleteById(IEnumerable<Guid> ids);
    Task BulkDelete(Expression<Func<TEntity, bool>> predicate);
    Task BulkDelete(IEnumerable<TEntity> entites);
    Task BulkUpdate(IEnumerable<TEntity> entites);
    Task BulkAdd(IEnumerable<TEntity> entites);
}
