using nexus_bilding_api.core.application.DTOs.Fiscal;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Fiscal.Queries;

public interface IGetFiscalDocumentByIdQueryHandler
{
    Task<Result<FiscalDocumentDto>> Handle(string id);
}

public class GetFiscalDocumentByIdQueryHandler : IGetFiscalDocumentByIdQueryHandler
{
    private readonly IFiscalDocumentRepository _repository;

    public GetFiscalDocumentByIdQueryHandler(IFiscalDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<FiscalDocumentDto>> Handle(string id)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id));
        if (entity == null) return Result<FiscalDocumentDto>.Fail($"Document Not Found {id}");

        return Result<FiscalDocumentDto>.Ok(FiscalMapper.ToDto(entity));
    }
}
