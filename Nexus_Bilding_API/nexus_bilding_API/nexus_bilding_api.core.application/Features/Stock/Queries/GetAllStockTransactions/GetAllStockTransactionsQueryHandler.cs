using nexus_bilding_api.core.application.DTOs.Stock;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Stock.Queries;

public interface IGetAllStockTransactionsQueryHandler
{
    Task<Result<IEnumerable<StockTransactionDto>>> Handle();
}

public class GetAllStockTransactionsQueryHandler : IGetAllStockTransactionsQueryHandler
{
    private readonly IStockTransactionRepository _repository;

    public GetAllStockTransactionsQueryHandler(IStockTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<StockTransactionDto>>> Handle()
    {
        var transactions = await _repository.GetAllAsync();
        var dtos = transactions.Select(StockMapper.ToDto);
        return Result<IEnumerable<StockTransactionDto>>.Ok(dtos);
    }
}
