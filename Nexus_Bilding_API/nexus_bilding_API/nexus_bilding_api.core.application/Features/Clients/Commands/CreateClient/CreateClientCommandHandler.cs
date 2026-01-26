using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Clients.Commands;

public interface ICreateClientCommandHandler
{
    Task<Result<ClientDto>> Handle(CreateClientDto command);
}

public class CreateClientCommandHandler : ICreateClientCommandHandler
{
    private readonly IClientRepository _repository;

    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ClientDto>> Handle(CreateClientDto command)
    {
        var entity = ClientMapper.ToEntity(command);
        await _repository.AddAsync(entity);
        return Result<ClientDto>.Ok(ClientMapper.ToDto(entity));
    }
}
