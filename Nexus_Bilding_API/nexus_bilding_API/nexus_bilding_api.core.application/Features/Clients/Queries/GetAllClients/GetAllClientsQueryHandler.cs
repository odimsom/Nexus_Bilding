using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Clients.Queries;

public interface IGetAllClientsQueryHandler
{
    Task<Result<IEnumerable<ClientDto>>> Handle();
}

public class GetAllClientsQueryHandler : IGetAllClientsQueryHandler
{
    private readonly IClientRepository _repository;

    public GetAllClientsQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ClientDto>>> Handle()
    {
        var clients = await _repository.GetAllAsync();
        var dtos = clients.Select(ClientMapper.ToDto);
        return Result<IEnumerable<ClientDto>>.Ok(dtos);
    }
}
