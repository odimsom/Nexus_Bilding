using AutoMapper;
using MediatR;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Queries;

public record GetUserSetupQuery(string UserId) : IRequest<OperationResult<UserSetupDto, DomainError>>;

public sealed class GetUserSetupQueryHandler : IRequestHandler<GetUserSetupQuery, OperationResult<UserSetupDto, DomainError>>
{
    private readonly IUserSetupRepository _userSetupRepository;
    private readonly IMapper _mapper;

    public GetUserSetupQueryHandler(IUserSetupRepository userSetupRepository, IMapper mapper)
    {
        _userSetupRepository = userSetupRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<UserSetupDto, DomainError>> Handle(GetUserSetupQuery request, CancellationToken cancellationToken)
    {
        var setup = await _userSetupRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (setup == null)
            return OperationResult<UserSetupDto, DomainError>.Fail(DomainError.NotFound("User.SetupNotFound", $"Setup not found for user {request.UserId}"));

        return OperationResult<UserSetupDto, DomainError>.Ok(_mapper.Map<UserSetupDto>(setup));
    }
}
