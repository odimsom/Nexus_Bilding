using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Clients.Commands;

public interface IUpdateClientCommandHandler
{
    Task<Result<string>> Handle(string id, CreateClientDto command);
}

public class UpdateClientCommandHandler : IUpdateClientCommandHandler
{
    private readonly IClientRepository _repository;

    public UpdateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(string id, CreateClientDto command)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<string>.Fail($"Client Not Found with id {id}");

        ClientMapper.UpdateEntity(entity, command);
        await _repository.UpdateAsync(entity);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
