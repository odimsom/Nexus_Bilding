using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Application.Ecf.Interfaces;

public interface IEcfService
{
    Task<OperationResult<string, DomainError>> GenerateSalesInvoiceXmlAsync(
        TenantIdentifier tenantId,
        SalesInvoiceHeader header,
        IEnumerable<SalesInvoiceLine> lines);

    Task<OperationResult<string, DomainError>> SignXmlAsync(
        string xml,
        string p12Path,
        string p12Password);

    Task<OperationResult<string, DomainError>> SendToDgiiAsync(
        string signedXml,
        string rnc,
        string environment);
}
