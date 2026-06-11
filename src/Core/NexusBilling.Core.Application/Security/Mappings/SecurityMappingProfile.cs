using AutoMapper;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Application.Security.Mappings;

public class SecurityMappingProfile : Profile
{
    public SecurityMappingProfile()
    {
        CreateMap<UserSetup, UserSetupDto>();
    }
}
