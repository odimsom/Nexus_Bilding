using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Products.Queries;

public interface IGetAllProductsQueryHandler
{
    Task<Result<IEnumerable<ProductDto>>> Handle();
}

public class GetAllProductsQueryHandler : IGetAllProductsQueryHandler
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle()
    {
        var products = await _repository.GetAllAsync();
        var dtos = products.Select(ProductMapper.ToDto);
        return Result<IEnumerable<ProductDto>>.Ok(dtos);
    }
}
