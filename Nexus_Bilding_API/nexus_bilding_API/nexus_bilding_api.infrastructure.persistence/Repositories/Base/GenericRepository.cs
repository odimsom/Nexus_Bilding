using Microsoft.EntityFrameworkCore;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.core.domain.Wrappers;

namespace nexus_bilding_api.infrastructure.persistence.Repositories.Base;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    public GenericRepository(DbContext Context)
    {
        _context = Context;
    }
    
    public virtual async Task<TEntity?> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<List<TEntity>?> AddRangeAsync(List<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities;
    }
    public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        // Need to find the entity first? Or just Attach/Update.
        // If entity is BaseEntity, we can get Id. But TEntity constraint is just class.
        // Assuming the entity passed is tracked or we attach it.
        // previous impl took id and entity.
         _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;

    }
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync(); //EF - immediate execution
    }
    public virtual async Task<List<TEntity>> GetAllListWithInclude(List<string> properties)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = properties.Aggregate(query, (current, property) => current.Include(property));

        return await query.ToListAsync();
    }
    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
    public virtual IQueryable<TEntity> GetAllQuery()
    {
        return _context.Set<TEntity>().AsQueryable();
    }
    public virtual IQueryable<TEntity> GetAllQueryWithInclude(List<string> properties)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        return properties.Aggregate(query, (current, property) => current.Include(property));
    }
    public virtual async Task<PaginatedResponse<TEntity>> GetPaginated(Pagination pagination)
    {
        var data = await _context.Set<TEntity>()
            .Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize).ToListAsync();
        return new PaginatedResponse<TEntity>
        {
            Data = data,
            Pagination = pagination
        };
    }
}