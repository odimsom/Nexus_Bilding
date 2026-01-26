using nexus_bilding_api.core.application.DTOs.Payment;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Payments.Commands;

public interface ICreatePaymentCommandHandler
{
    Task<Result<string>> Handle(CreatePaymentDto command);
}

public class CreatePaymentCommandHandler : ICreatePaymentCommandHandler
{
    private readonly IPaymentRepository _repository;

    public CreatePaymentCommandHandler(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(CreatePaymentDto command)
    {
        var entity = PaymentMapper.ToEntity(command);
        await _repository.AddAsync(entity);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
