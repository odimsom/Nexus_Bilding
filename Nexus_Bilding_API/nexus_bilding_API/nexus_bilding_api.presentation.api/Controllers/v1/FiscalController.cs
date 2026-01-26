using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Fiscal;
using nexus_bilding_api.core.application.Features.Fiscal.Commands;
using nexus_bilding_api.core.application.Features.Fiscal.Queries;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class FiscalController : BaseApiController
{
    private readonly ICreateFiscalDocumentCommandHandler _createHandler;
    private readonly IGetAllFiscalDocumentsQueryHandler _getAllHandler;
    private readonly IGetFiscalDocumentByIdQueryHandler _getByIdHandler;

    public FiscalController(
        ICreateFiscalDocumentCommandHandler createHandler,
        IGetAllFiscalDocumentsQueryHandler getAllHandler,
        IGetFiscalDocumentByIdQueryHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await _getAllHandler.Handle());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return HandleResult(await _getByIdHandler.Handle(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFiscalDocumentDto command)
    {
        return HandleResult(await _createHandler.Handle(command));
    }
}
