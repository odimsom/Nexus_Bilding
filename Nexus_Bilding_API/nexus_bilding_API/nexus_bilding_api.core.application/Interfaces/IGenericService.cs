using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Interfaces;

public interface IGenericService<T> where T : class
{
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(string id);
    Task<Result<T>> AddAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
    Task<Result<T>> DeleteAsync(string id);
}
