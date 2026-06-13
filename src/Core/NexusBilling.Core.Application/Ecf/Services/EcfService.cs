using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using NexusBilling.Core.Application.Ecf.Interfaces;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Application.Ecf.Services;

public class EcfService : IEcfService
{
    public Task<OperationResult<string, DomainError>> GenerateSalesInvoiceXmlAsync(
        TenantIdentifier tenantId,
        SalesInvoiceHeader header,
        IEnumerable<SalesInvoiceLine> lines)
    {
        try
        {
            var ecfType = MapNcfToEcfType(header.NoSeries); // Simplificación: usar la serie
            
            // Si el No. ya empieza con E, es que ya se asignó un NCF electrónico
            var ncf = header.No; 
            if (!ncf.StartsWith("E"))
            {
                // En un flujo real, aquí se consumiría la secuencia ECF
                // Por ahora simulamos el prefijo
                ncf = "E" + ecfType + ncf.PadLeft(10, '0').Substring(ncf.Length > 10 ? ncf.Length - 10 : 0);
            }

            var doc = new XDocument(
                new XElement("RFCE",
                    new XElement("Encabezado",
                        new XElement("Version", "1.0"),
                        new XElement("IdDoc",
                            new XElement("TipoeCF", ecfType),
                            new XElement("eNCF", ncf),
                            new XElement("FechaVencimientoSecuencia", "31-12-2026"), // TODO: Leer de la secuencia
                            new XElement("IndicadorMontoGravado", "1"),
                            new XElement("TipoIngresos", "01"),
                            new XElement("TipoPago", header.PaymentTermsCode?.Contains("CONTADO") == true ? "1" : "2")
                        ),
                        new XElement("Emisor",
                            new XElement("RNCEmisor", "101234567"), // TODO: Leer de CompanyInformation
                            new XElement("RazonSocialEmisor", "Mi Empresa SRL"), // TODO: Leer de CompanyInformation
                            new XElement("DireccionEmisor", header.SellToAddress ?? ""),
                            new XElement("FechaEmision", header.PostingDate?.ToString("dd-MM-yyyy") ?? DateTime.UtcNow.ToString("dd-MM-yyyy"))
                        ),
                        new XElement("Comprador",
                            new XElement("RNCComprador", header.VatRegistrationNo ?? ""),
                            new XElement("RazonSocialComprador", header.BillToName ?? "")
                        ),
                        new XElement("Totales",
                            new XElement("MontoGravadoTotal", lines.Where(l => l.Vat > 0).Sum(l => l.Amount).ToString("F2", CultureInfo.InvariantCulture)),
                            new XElement("MontoITBIS1", lines.Sum(l => l.Vat).ToString("F2", CultureInfo.InvariantCulture)),
                            new XElement("MontoTotal", lines.Sum(l => l.AmountIncludingVat).ToString("F2", CultureInfo.InvariantCulture))
                        )
                    ),
                    new XElement("DetallesItems",
                        lines.Select((l, i) => new XElement("Item",
                            new XAttribute("NumLinea", i + 1),
                            new XElement("NombreItem", l.Description),
                            new XElement("CantidadItem", l.Quantity.ToString("F2", CultureInfo.InvariantCulture)),
                            new XElement("UnidadMedida", l.UnitOfMeasureCode ?? "UND"),
                            new XElement("PrecioUnitarioItem", l.UnitPrice.ToString("F2", CultureInfo.InvariantCulture)),
                            new XElement("TablaSubcotizacion", l.Vat > 0 ? "18" : "0"),
                            new XElement("MontoItem", l.Amount.ToString("F2", CultureInfo.InvariantCulture))
                        ))
                    )
                )
            );

            return Task.FromResult(OperationResult<string, DomainError>.Ok(doc.ToString()));
        }
        catch (Exception ex)
        {
            return Task.FromResult(OperationResult<string, DomainError>.Fail(DomainError.Business("ecf.xml_gen_failed", $"Error generando XML: {ex.Message}")));
        }
    }

    public Task<OperationResult<string, DomainError>> SignXmlAsync(
        string xml,
        string p12Path,
        string p12Password)
    {
        try
        {
            // Usar X509CertificateLoader en lugar de constructor obsoleto
            using var cert = X509CertificateLoader.LoadPkcs12(System.IO.File.ReadAllBytes(p12Path), p12Password);
            
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xml);

            var signedXml = new SignedXml(xmlDoc)
            {
                SigningKey = cert.GetRSAPrivateKey()
            };

            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());
            signedXml.AddReference(reference);

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            signedXml.KeyInfo = keyInfo;

            signedXml.ComputeSignature();
            var xmlDigitalSignature = signedXml.GetXml();

            xmlDoc.DocumentElement?.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return Task.FromResult(OperationResult<string, DomainError>.Ok(xmlDoc.OuterXml));
        }
        catch (Exception ex)
        {
            return Task.FromResult(OperationResult<string, DomainError>.Fail(DomainError.Business("ecf.sign_failed", $"Error firmando XML: {ex.Message}")));
        }
    }

    public async Task<OperationResult<string, DomainError>> SendToDgiiAsync(
        string signedXml,
        string rnc,
        string environment)
    {
        try
        {
            var url = environment.ToLower() == "production" 
                ? "https://ecf.dgii.gov.do/ecf/recepcion" 
                : "https://test.dgii.gov.do/ecf/recepcion";

            using var client = new HttpClient();
            var content = new StringContent(signedXml, System.Text.Encoding.UTF8, "application/xml");
            
            client.DefaultRequestHeaders.Add("rnc", rnc);

            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return OperationResult<string, DomainError>.Ok(responseContent); 
            }

            return OperationResult<string, DomainError>.Fail(DomainError.Business("dgii.submission_failed", $"DGII Error: {response.StatusCode} - {responseContent}"));
        }
        catch (Exception ex)
        {
            return OperationResult<string, DomainError>.Fail(DomainError.Business("dgii.connection_error", $"Error conectando con DGII: {ex.Message}"));
        }
    }

    private string MapNcfToEcfType(string noSeries)
    {
        if (string.IsNullOrEmpty(noSeries)) return "31";
        if (noSeries.Contains("B01") || noSeries.Contains("E31")) return "31";
        if (noSeries.Contains("B02") || noSeries.Contains("E32")) return "32";
        if (noSeries.Contains("B04") || noSeries.Contains("E34")) return "34";
        return "31";
    }
}
