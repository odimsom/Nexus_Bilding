using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Enums;
using nexus_bilding_api.infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace nexus_bilding_api.infrastructure.persistence.Seeds;

public static class DefaultSeeds
{
    public static async Task SeedAsync(NexusBillingContext context)
    {
        // Clients
        if (!await context.Clients.AnyAsync())
        {
            var clients = new List<Client>
            {
                new Client
                {
                   // Id will be generated or we can set it if GUID
                   Name = "Acme Studio S.R.L.",
                   Email = "billing@acmestudio.com",
                   Phone = "+1 (809) 555-0123",
                   TaxId = "131-44930-2",
                   IsCompany = true,
                   Address = "Av. Winston Churchill #45",
                   City = "Santo Domingo",
                   Status = ClientStatus.Active,
                   VerifiedAt = DateTime.UtcNow.AddMonths(-10),
                   Notes = "Cliente B2B recurrente.",
                   CreatedAt = DateTime.UtcNow.AddMonths(-10)
                },
                new Client
                {
                   Name = "Juan Pérez",
                   Email = "juan.perez@email.com",
                   Phone = "+1 (829) 555-4567",
                   TaxId = "001-0022334-5",
                   IsCompany = false,
                   Address = "Calle Sol #123",
                   City = "Santiago",
                   Status = ClientStatus.Active,
                   CreatedAt = DateTime.UtcNow.AddMonths(-9)
                }
            };
            await context.Clients.AddRangeAsync(clients);
            await context.SaveChangesAsync();
        }

        // Products
        if (!await context.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Silla Ergonómica",
                    Description = "Silla de oficina con soporte lumbar.",
                    Sku = "FUR-OFF-004",
                    Category = "Mobiliario",
                    Price = 15000,
                    Cost = 8500,
                        ItbisRate = ItbisRate.Rate18,
                    Stock = 42,
                    LowStockThreshold = 10,
                    Status = ProductStatus.Active,
                    CreatedAt = DateTime.UtcNow.AddMonths(-10)
                },
                new Product
                {
                    Name = "Servicio Consultoría IT",
                    Category = "Servicios",
                    Price = 3500,
                        ItbisRate = ItbisRate.Rate18,
                    Status = ProductStatus.Active,
                    CreatedAt = DateTime.UtcNow.AddMonths(-10)
                }
            };
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        // Retrieve seeded entities for relationships
        var client1 = await context.Clients.FirstOrDefaultAsync(c => c.TaxId == "131-44930-2");
        var client2 = await context.Clients.FirstOrDefaultAsync(c => c.TaxId == "001-0022334-5");
        var product1 = await context.Products.FirstOrDefaultAsync(p => p.Name == "Silla Ergonómica");

        // Stock Transactions
        if (!await context.StockTransactions.AnyAsync() && product1 != null)
        {
            var transaction = new StockTransaction
            {
                ProductId = product1.Id,
                Type = StockTransactionType.Purchase,
                Quantity = 50,
                BalanceAfter = 50,
                Reference = "PO-001",
                CreatedAt = DateTime.UtcNow.AddMonths(-3)
            };
            // Note: In real app, adding transaction should update Product Stock, but here we assume Product Stock is already set or managed.
            // Since we set Stock=42 in Product seed (maybe result of transactions), we just log this.
            await context.StockTransactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        }

        // Fiscal Documents
        if (!await context.FiscalDocuments.AnyAsync() && client1 != null && client2 != null && product1 != null)
        {
            var doc1 = new FiscalDocument
            {
                DocumentType = FiscalDocumentType.E31, // E31
                ECFNumber = "E310000000001",
                ClientId = client1.Id,
                ClientName = client1.Name,
                ClientTaxId = client1.TaxId,
                IssueDate = DateTime.UtcNow.AddDays(-60),
                DueDate = DateTime.UtcNow.AddDays(-30),
                Items = new List<FiscalDocumentItem>
                 {
                     new FiscalDocumentItem
                     {
                         ProductId = product1.Id,
                         Description = product1.Name,
                         Quantity = 2,
                         UnitPrice = 15000,
                             ItbisRate = ItbisRate.Rate18,
                         ItbisAmount = 5400, // 2 * 15000 * 0.18
                         Total = 35400 // (30000) + 5400
                     }
                 },
                Subtotal = 30000,
                TotalITBIS = 5400,
                TotalAmount = 35400,
                Status = FiscalDocumentStatus.Accepted,
                CreatedAt = DateTime.UtcNow.AddDays(-60)
            };

            var doc2 = new FiscalDocument
            {
                DocumentType = FiscalDocumentType.E32, // E32
                ClientId = client2.Id,
                ClientName = client2.Name,
                ClientTaxId = client2.TaxId,
                IssueDate = DateTime.UtcNow.AddDays(-5),
                Items = new List<FiscalDocumentItem>
                 {
                     new FiscalDocumentItem
                     {
                         Description = "Servicio de Consultoría", // Product 2, effectively
                         Quantity = 1,
                         UnitPrice = 5000,
                             ItbisRate = ItbisRate.Rate18,
                         ItbisAmount = 900,
                         Total = 5900
                     }
                 },
                Subtotal = 5000,
                TotalITBIS = 900,
                TotalAmount = 5900,
                Status = FiscalDocumentStatus.Draft,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            };

            await context.FiscalDocuments.AddRangeAsync(doc1, doc2);
            await context.SaveChangesAsync();
        }
    }
}
