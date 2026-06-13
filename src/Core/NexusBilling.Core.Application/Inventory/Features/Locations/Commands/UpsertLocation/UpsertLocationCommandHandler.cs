using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using System.Reflection;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Commands.UpsertLocation;

public sealed class UpsertLocationCommandHandler(ILocationRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpsertLocationCommand, UpsertLocationResult>
{
    public async Task<UpsertLocationResult> Handle(UpsertLocationCommand request, CancellationToken cancellationToken)
    {
        var tid = TenantIdentifier.Create(request.TenantId);
        
        var locations = await repo.FindAsync(l => l.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(request.TenantId) && l.Code == (request.OriginalCode ?? request.Code), null, cancellationToken);
        var location = locations.FirstOrDefault();

        bool created = false;

        if (location == null)
        {
            var result = Location.Create(tid, request.Code, request.Name, request.Address, request.City);
            if (!result.IsSuccess)
                throw new InvalidOperationException(result.GetError()?.Message ?? "Error creating location.");
                
            location = result.GetValue()!;
            await repo.AddAsync(location, cancellationToken);
            created = true;
        }
        else
        {
            SetProperty(location, "Code", request.Code);
            SetProperty(location, "Name", request.Name);
            SetProperty(location, "Address", request.Address);
            SetProperty(location, "City", request.City);
            await repo.UpdateAsync(location, cancellationToken);
        }

        await uow.SaveChangesAsync(cancellationToken);
        return new UpsertLocationResult(location.Code, created);
    }
    
    private void SetProperty(object obj, string propertyName, object value)
    {
        var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, value);
        }
        else
        {
            var field = obj.GetType().GetField($"<{propertyName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
    }
}
