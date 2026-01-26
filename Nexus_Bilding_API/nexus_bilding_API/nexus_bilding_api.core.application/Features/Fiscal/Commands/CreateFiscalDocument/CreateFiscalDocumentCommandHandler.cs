using nexus_bilding_api.core.application.DTOs.Fiscal;
using nexus_bilding_api.core.application.Mappings;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Enums;
using nexus_bilding_api.core.domain.Interfaces;

namespace nexus_bilding_api.core.application.Features.Fiscal.Commands;

public interface ICreateFiscalDocumentCommandHandler
{
    Task<Result<string>> Handle(CreateFiscalDocumentDto command);
}

public class CreateFiscalDocumentCommandHandler : ICreateFiscalDocumentCommandHandler
{
    private readonly IFiscalDocumentRepository _repository;

    public CreateFiscalDocumentCommandHandler(IFiscalDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(CreateFiscalDocumentDto command)
    {
        var entity = FiscalMapper.ToEntity(command);

        // Calculate totals from items
        decimal subtotal = 0;
        decimal totalTax = 0;
        decimal totalDiscount = 0;

        foreach (var itemDto in command.Items)
        {
            var item = new FiscalDocumentItem
            {
                ProductId = itemDto.ProductId != null ? Guid.Parse(itemDto.ProductId) : null,
                Description = itemDto.Description,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                ItbisRate = ItbisRate.Exempt, // Default or map from dto.TaxRate if logic permits
                ItbisAmount = 0, // Calculate based on Rate * Amount
                Discount = itemDto.Discount,
                Total = ((decimal)itemDto.Quantity * itemDto.UnitPrice) - itemDto.Discount
            };

            // Simple calculation logic (Refine as per business rules)
            item.Total = ((decimal)item.Quantity * item.UnitPrice) - (item.Discount ?? 0);

            entity.Items.Add(item);

            subtotal += ((decimal)item.Quantity * item.UnitPrice);
            totalDiscount += (item.Discount ?? 0);
            totalTax += item.ItbisAmount;
        }

        entity.Subtotal = subtotal;
        entity.TotalDiscount = totalDiscount;
        entity.TotalITBIS = totalTax;
        entity.TotalAmount = subtotal - totalDiscount + totalTax;

        await _repository.AddAsync(entity);
        return Result<string>.Ok(entity.Id.ToString());
    }
}
