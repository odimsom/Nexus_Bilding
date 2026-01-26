using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Stock;
using nexus_bilding_api.core.application.Features.Stock.Commands;
using nexus_bilding_api.core.application.Features.Stock.Queries;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class StockController : BaseApiController
{
    private readonly ICreateStockTransactionCommandHandler _createHandler;
    private readonly IGetAllStockTransactionsQueryHandler _getAllHandler;

    public StockController(
        ICreateStockTransactionCommandHandler createHandler,
        IGetAllStockTransactionsQueryHandler getAllHandler)
    {
         _createHandler = createHandler;
         _getAllHandler = getAllHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await _getAllHandler.Handle());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStockTransactionDto command)
    {
        return HandleResult(await _createHandler.Handle(command));
    }
}
