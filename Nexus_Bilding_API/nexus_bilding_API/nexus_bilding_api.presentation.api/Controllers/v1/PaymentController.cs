using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Payment;
using nexus_bilding_api.core.application.Features.Payments.Commands;
using nexus_bilding_api.core.application.Features.Payments.Queries;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class PaymentController : BaseApiController
{
    private readonly ICreatePaymentCommandHandler _createHandler;
    private readonly IGetAllPaymentsQueryHandler _getAllHandler;

    public PaymentController(
        ICreatePaymentCommandHandler createHandler,
        IGetAllPaymentsQueryHandler getAllHandler)
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
    public async Task<IActionResult> Create(CreatePaymentDto command)
    {
        return HandleResult(await _createHandler.Handle(command));
    }
}
