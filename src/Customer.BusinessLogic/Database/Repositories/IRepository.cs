using Customer.BusinessLogic.Database.Entities;
using System.Linq.Expressions;

namespace Customer.DatabaseLogic.Repositories
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<Guid> DeleteAsync(Guid id);
        Task<Guid> DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAllAsync();
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, int chunkSize = 1000, bool consoleOutput = true);

    }
}
