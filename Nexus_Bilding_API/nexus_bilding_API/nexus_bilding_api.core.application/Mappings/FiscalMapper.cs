using nexus_bilding_api.core.application.DTOs.Fiscal;
using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.Mappings;

public static class FiscalMapper
{
    public static FiscalDocumentDto ToDto(FiscalDocument entity)
    {
        return new FiscalDocumentDto
        {
            Id = entity.Id.ToString(),
            DocumentType = entity.DocumentType,
            ECFNumber = entity.ECFNumber,
            ClientId = entity.ClientId?.ToString(),
            ClientName = entity.ClientName,
            ClientTaxId = entity.ClientTaxId,
            IssueDate = entity.IssueDate,
            DueDate = entity.DueDate,
            Items = entity.Items.Select(ToItemDto).ToList(),
            Subtotal = entity.Subtotal,
            TotalITBIS = entity.TotalITBIS,
            TotalDiscount = entity.TotalDiscount,
            TotalAmount = entity.TotalAmount,
            Status = entity.Status,
            SecurityCode = entity.SecurityCode, // Assuming this exists in Entity or will be added
            XmlUrl = null, // Not in Entity yet?
            QrCodeContent = null, // Not in Entity yet?
            SignedAt = null, // Not in Entity yet?
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt
        };
    }

    private static FiscalDocumentItemDto ToItemDto(FiscalDocumentItem item)
    {
        return new FiscalDocumentItemDto
        {
            ProductId = item.ProductId.ToString() ?? string.Empty,
            Description = item.Description,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TaxRate = (decimal)item.ItbisRate, // Enum to decimal cast or map if needed
            Discount = item.Discount ?? 0,
            Total = item.Total
        };
    }

    public static FiscalDocument ToEntity(CreateFiscalDocumentDto dto)
    {
        var doc = new FiscalDocument
        {
            DocumentType = dto.DocumentType,
            ClientId = dto.ClientId != null ? Guid.Parse(dto.ClientId) : null,
            ClientName = dto.ClientName,
            ClientTaxId = dto.ClientTaxId ?? string.Empty,
            IssueDate = dto.IssueDate,
            DueDate = dto.DueDate,
            Notes = dto.Notes,
            Status = FiscalDocumentStatus.Draft // Default status
        };

        // Items must be added separately or mapped here if collection exists
        return doc;
    }
}
