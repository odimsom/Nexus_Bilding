using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Products.Commands;

public interface ICreateProductCommandHandler
{
    Task<Result<ProductDto>> Handle(CreateProductDto command);
}

public class CreateProductCommandHandler : ICreateProductCommandHandler
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductDto>> Handle(CreateProductDto command)
    {
        var entity = ProductMapper.ToEntity(command);
        await _repository.AddAsync(entity);
        return Result<ProductDto>.Ok(ProductMapper.ToDto(entity));
    }
}
