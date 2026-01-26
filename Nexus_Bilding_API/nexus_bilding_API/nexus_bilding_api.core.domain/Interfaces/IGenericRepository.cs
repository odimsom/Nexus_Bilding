using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Wrappers;

namespace nexus_bilding_api.core.domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> AddAsync(TEntity entity);
    Task<List<TEntity>?> AddRangeAsync(List<TEntity> entities);
    Task DeleteAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllListWithInclude(List<string> properties);
    IQueryable<TEntity> GetAllQuery();
    IQueryable<TEntity> GetAllQueryWithInclude(List<string> properties);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<PaginatedResponse<TEntity>> GetPaginated(Pagination pagination);
}
