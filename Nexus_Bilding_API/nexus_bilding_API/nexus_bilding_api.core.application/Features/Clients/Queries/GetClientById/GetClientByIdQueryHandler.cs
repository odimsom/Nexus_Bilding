using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Clients.Queries;

public interface IGetClientByIdQueryHandler
{
    Task<Result<ClientDto>> Handle(string id);
}

public class GetClientByIdQueryHandler : IGetClientByIdQueryHandler
{
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ClientDto>> Handle(string id)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<ClientDto>.Fail($"Client Not Found with id {id}");
        return Result<ClientDto>.Ok(ClientMapper.ToDto(entity));
    }
}
