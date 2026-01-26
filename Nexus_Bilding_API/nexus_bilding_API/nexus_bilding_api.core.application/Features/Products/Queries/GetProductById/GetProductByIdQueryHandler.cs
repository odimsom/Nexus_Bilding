using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Products.Queries;

public interface IGetProductByIdQueryHandler
{
    Task<Result<ProductDto>> Handle(string id);
}

public class GetProductByIdQueryHandler : IGetProductByIdQueryHandler
{
    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductDto>> Handle(string id)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<ProductDto>.Fail($"Product Not Found with id {id}");
        return Result<ProductDto>.Ok(ProductMapper.ToDto(entity));
    }
}
