using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Ecf.Repositories;

public interface IEcfCompanyConfigRepository : IGenericRepository<EcfCompanyConfig>
{
}

public interface IEcfNcfSequenceRepository : IGenericRepository<EcfNcfSequence>
{
}

public interface IEcfDocumentRepository : IGenericRepository<EcfDocument>
{
}
