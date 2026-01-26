using nexus_bilding_api.core.application.DTOs.Stock;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Stock.Commands;

public interface ICreateStockTransactionCommandHandler
{
    Task<Result<string>> Handle(CreateStockTransactionDto command);
}

public class CreateStockTransactionCommandHandler : ICreateStockTransactionCommandHandler
{
    private readonly IStockTransactionRepository _repository;

    public CreateStockTransactionCommandHandler(IStockTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(CreateStockTransactionDto command)
    {
        var entity = StockMapper.ToEntity(command);
        await _repository.AddAsync(entity);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
