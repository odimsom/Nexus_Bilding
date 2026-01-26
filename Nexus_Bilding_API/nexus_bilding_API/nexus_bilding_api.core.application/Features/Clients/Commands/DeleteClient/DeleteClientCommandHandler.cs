using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Clients.Commands;

public interface IDeleteClientCommandHandler
{
    Task<Result<string>> Handle(string id);
}

public class DeleteClientCommandHandler : IDeleteClientCommandHandler
{
    private readonly IClientRepository _repository;

    public DeleteClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(string id)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<string>.Fail($"Client Not Found with id {id}");

        await _repository.DeleteAsync(entity.Id);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
