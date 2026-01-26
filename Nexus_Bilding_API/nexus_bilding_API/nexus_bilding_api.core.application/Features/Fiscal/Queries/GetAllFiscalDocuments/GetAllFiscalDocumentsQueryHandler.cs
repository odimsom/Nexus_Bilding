using nexus_bilding_api.core.application.DTOs.Fiscal;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Fiscal.Queries;

public interface IGetAllFiscalDocumentsQueryHandler
{
    Task<Result<IEnumerable<FiscalDocumentDto>>> Handle();
}

public class GetAllFiscalDocumentsQueryHandler : IGetAllFiscalDocumentsQueryHandler
{
    private readonly IFiscalDocumentRepository _repository;

    public GetAllFiscalDocumentsQueryHandler(IFiscalDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<FiscalDocumentDto>>> Handle()
    {
        var docs = await _repository.GetAllAsync(); 
        // Note: Generic Repository might not include Items (Include). 
        // Ideally we need a specific method in Repository or ensure Generic includes navigations if needed.
        // For list view, maybe items aren't needed.
        
        var dtos = docs.Select(FiscalMapper.ToDto);
        return Result<IEnumerable<FiscalDocumentDto>>.Ok(dtos);
    }
}
