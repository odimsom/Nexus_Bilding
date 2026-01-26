using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.application.Features.Clients.Commands;
using nexus_bilding_api.core.application.Features.Clients.Queries;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class ClientController : BaseApiController
{
    private readonly ICreateClientCommandHandler _createHandler;
    private readonly IUpdateClientCommandHandler _updateHandler;
    private readonly IDeleteClientCommandHandler _deleteHandler;
    private readonly IGetAllClientsQueryHandler _getAllHandler;
    private readonly IGetClientByIdQueryHandler _getByIdHandler;

    public ClientController(
        ICreateClientCommandHandler createHandler,
        IUpdateClientCommandHandler updateHandler,
        IDeleteClientCommandHandler deleteHandler,
        IGetAllClientsQueryHandler getAllHandler,
        IGetClientByIdQueryHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
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
    public async Task<IActionResult> Create(CreateClientDto command)
    {
        return HandleResult(await _createHandler.Handle(command));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateClientDto command)
    {
        // Ideally command should not contain Id if passed in URL, but let's assume body doesn't override logic or handler handles it.
        // Handler takes id and command.
        return HandleResult(await _updateHandler.Handle(id, command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return HandleResult(await _deleteHandler.Handle(id));
    }
}
