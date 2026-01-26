using nexus_bilding_api.core.application.DTOs.Client;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.core.application.Mappings;

public static class ClientMapper
{
    public static ClientDto ToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id.ToString(),
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            TaxId = client.TaxId,
            IsCompany = client.IsCompany,
            Address = client.Address,
            City = client.City,
            Status = client.Status,
            VerifiedAt = client.VerifiedAt,
            Notes = client.Notes,
            CreatedAt = client.CreatedAt.ToString("yyyy-MM-dd") ?? string.Empty
        };
    }

    public static Client ToEntity(CreateClientDto dto)
    {
        return new Client
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            TaxId = dto.TaxId,
            IsCompany = dto.IsCompany,
            Address = dto.Address,
            City = dto.City,
            Notes = dto.Notes,
            Status = nexus_bilding_api.core.domain.Enums.ClientStatus.Active // Default to Active
        };
    }

    public static void UpdateEntity(Client client, CreateClientDto dto)
    {
        client.Name = dto.Name;
        client.Email = dto.Email;
        client.Phone = dto.Phone;
        client.TaxId = dto.TaxId;
        client.IsCompany = dto.IsCompany;
        client.Address = dto.Address;
        client.City = dto.City;
        client.Notes = dto.Notes;
    }
}
