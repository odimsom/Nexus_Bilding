using NexusBilling.Api;
using NexusBilling.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Registro modular de servicios
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddPersistenceInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
