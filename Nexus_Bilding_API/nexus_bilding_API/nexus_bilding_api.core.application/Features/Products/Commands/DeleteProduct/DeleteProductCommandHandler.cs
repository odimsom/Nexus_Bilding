using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Products.Commands;

public interface IDeleteProductCommandHandler
{
    Task<Result<string>> Handle(string id);
}

public class DeleteProductCommandHandler : IDeleteProductCommandHandler
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(string id)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<string>.Fail($"Product Not Found with id {id}");

        await _repository.DeleteAsync(entity.Id);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
