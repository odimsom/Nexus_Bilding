using nexus_bilding_api.core.application.DTOs.Payment;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Payments.Queries;

public interface IGetAllPaymentsQueryHandler
{
    Task<Result<IEnumerable<PaymentDto>>> Handle();
}

public class GetAllPaymentsQueryHandler : IGetAllPaymentsQueryHandler
{
    private readonly IPaymentRepository _repository;

    public GetAllPaymentsQueryHandler(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<PaymentDto>>> Handle()
    {
        var payments = await _repository.GetAllAsync();
        var dtos = payments.Select(PaymentMapper.ToDto);
        return Result<IEnumerable<PaymentDto>>.Ok(dtos);
    }
}
