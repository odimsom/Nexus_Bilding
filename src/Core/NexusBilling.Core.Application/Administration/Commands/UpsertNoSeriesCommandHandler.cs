using MediatR;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Administration.Commands;

public sealed class UpsertNoSeriesCommandHandler(INoSeriesRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpsertNoSeriesCommand, bool>
{
    public async Task<bool> Handle(UpsertNoSeriesCommand cmd, CancellationToken ct)
    {
        var existing = await repo.GetByCodeForTenantAsync(cmd.TenantId, cmd.Code, ct);
        if (existing is not null)
        {
            existing.Description = cmd.Description;
            existing.DefaultNos = cmd.DefaultNos;
            existing.ManualNos = cmd.ManualNos;
            existing.UpdatedAt = DateTime.UtcNow;
            await repo.UpdateAsync(existing, ct);
        }
        else
        {
            var result = NoSeries.Create(TenantIdentifier.Create(cmd.TenantId), cmd.Code, cmd.Description);
            if (!result.IsSuccess) return false;
            var ns = result.GetValue()!;
            ns.DefaultNos = cmd.DefaultNos;
            ns.ManualNos = cmd.ManualNos;
            await repo.AddAsync(ns, ct);
        }
        await uow.SaveChangesAsync(ct);
        return true;
    }
}

public sealed class UpsertNoSeriesLineCommandHandler(INoSeriesLineRepository lineRepo, IUnitOfWork uow)
    : IRequestHandler<UpsertNoSeriesLineCommand, bool>
{
    public async Task<bool> Handle(UpsertNoSeriesLineCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);
        var existing = await lineRepo.GetActiveLineAsync(cmd.TenantId, cmd.SeriesCode, ct);
        if (existing is not null)
        {
            existing.StartingNo = cmd.StartingNo;
            existing.EndingNo = cmd.EndingNo;
            existing.IncrementByNo = cmd.IncrementByNo;
            existing.UpdatedAt = DateTime.UtcNow;
            await lineRepo.UpdateAsync(existing, ct);
        }
        else
        {
            var result = NoSeriesLine.Create(tid, cmd.SeriesCode, cmd.StartingNo, cmd.EndingNo);
            if (!result.IsSuccess) return false;
            var line = result.GetValue()!;
            line.IncrementByNo = cmd.IncrementByNo;
            await lineRepo.AddAsync(line, ct);
        }
        await uow.SaveChangesAsync(ct);
        return true;
    }
}

public sealed class DeleteNoSeriesCommandHandler(INoSeriesRepository repo, INoSeriesLineRepository lineRepo, IUnitOfWork uow)
    : IRequestHandler<DeleteNoSeriesCommand, bool>
{
    public async Task<bool> Handle(DeleteNoSeriesCommand cmd, CancellationToken ct)
    {
        var existing = await repo.GetByCodeForTenantAsync(cmd.TenantId, cmd.Code, ct);
        if (existing is null) return false;
        var lines = await lineRepo.GetLinesForSeriesAsync(cmd.TenantId, cmd.Code, ct);
        foreach (var l in lines) await lineRepo.DeleteAsync(l, ct);
        await repo.DeleteAsync(existing, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
