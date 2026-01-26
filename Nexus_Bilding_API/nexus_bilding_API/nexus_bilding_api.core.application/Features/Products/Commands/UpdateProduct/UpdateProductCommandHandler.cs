using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Products.Commands;

public interface IUpdateProductCommandHandler
{
    Task<Result<string>> Handle(string id, CreateProductDto command);
}

public class UpdateProductCommandHandler : IUpdateProductCommandHandler
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(string id, CreateProductDto command)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<string>.Fail($"Product Not Found with id {id}");

        ProductMapper.UpdateEntity(entity, command);
        await _repository.UpdateAsync(entity);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
