using Microsoft.Extensions.DependencyInjection;
using nexus_bilding_api.core.application.Features.Clients.Commands;
using nexus_bilding_api.core.application.Features.Clients.Queries;
using nexus_bilding_api.core.application.Features.Products.Commands;
using nexus_bilding_api.core.application.Features.Products.Queries;
using nexus_bilding_api.core.application.Features.Users.Commands.Authenticate;
using nexus_bilding_api.core.application.Features.Users.Commands.Register;
using nexus_bilding_api.core.application.Features.Fiscal.Commands;
using nexus_bilding_api.core.application.Features.Fiscal.Queries;
using nexus_bilding_api.core.application.Features.Payments.Commands;
using nexus_bilding_api.core.application.Features.Payments.Queries;
using nexus_bilding_api.core.application.Features.Stock.Commands;
using nexus_bilding_api.core.application.Features.Stock.Queries;

namespace nexus_bilding_api.core.application;

public static class ServiceRegistration
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // Clients
        services.AddScoped<ICreateClientCommandHandler, CreateClientCommandHandler>();
        services.AddScoped<IUpdateClientCommandHandler, UpdateClientCommandHandler>();
        services.AddScoped<IDeleteClientCommandHandler, DeleteClientCommandHandler>();
        services.AddScoped<IGetAllClientsQueryHandler, GetAllClientsQueryHandler>();
        services.AddScoped<IGetClientByIdQueryHandler, GetClientByIdQueryHandler>();

        // Products
        services.AddScoped<ICreateProductCommandHandler, CreateProductCommandHandler>();
        services.AddScoped<IUpdateProductCommandHandler, UpdateProductCommandHandler>();
        services.AddScoped<IDeleteProductCommandHandler, DeleteProductCommandHandler>();
        services.AddScoped<IGetAllProductsQueryHandler, GetAllProductsQueryHandler>();
        services.AddScoped<IGetProductByIdQueryHandler, GetProductByIdQueryHandler>();

        // Users
        services.AddScoped<IAuthenticateUserCommandHandler, AuthenticateUserCommandHandler>();
        services.AddScoped<IRegisterUserCommandHandler, RegisterUserCommandHandler>();
        services.AddScoped<nexus_bilding_api.core.application.Features.Users.Commands.ConfirmEmail.IConfirmEmailCommandHandler, nexus_bilding_api.core.application.Features.Users.Commands.ConfirmEmail.ConfirmEmailCommandHandler>();

        // Fiscal Documents
        services.AddScoped<ICreateFiscalDocumentCommandHandler, CreateFiscalDocumentCommandHandler>();
        services.AddScoped<IGetAllFiscalDocumentsQueryHandler, GetAllFiscalDocumentsQueryHandler>();
        services.AddScoped<IGetFiscalDocumentByIdQueryHandler, GetFiscalDocumentByIdQueryHandler>();

        // Payments
        services.AddScoped<ICreatePaymentCommandHandler, CreatePaymentCommandHandler>();
        services.AddScoped<IGetAllPaymentsQueryHandler, GetAllPaymentsQueryHandler>();

        // Stock Transactions
        services.AddScoped<ICreateStockTransactionCommandHandler, CreateStockTransactionCommandHandler>();
        services.AddScoped<IGetAllStockTransactionsQueryHandler, GetAllStockTransactionsQueryHandler>();
    }
}
