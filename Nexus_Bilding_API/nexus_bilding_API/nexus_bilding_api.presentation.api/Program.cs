using nexus_bilding_api.infrastructure.persistence;
using nexus_bilding_api.infrastructure.identity;
using nexus_bilding_api.core.application;
using nexus_bilding_api.infrastructure.shared;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceLayerIoc(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<nexus_bilding_api.infrastructure.persistence.Context.NexusBillingContext>();
        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<nexus_bilding_api.infrastructure.identity.Entities.AppUser>>();
        var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
        
        await nexus_bilding_api.infrastructure.identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
        await nexus_bilding_api.infrastructure.identity.Seeds.DefaultAdminUser.SeedAsync(userManager, roleManager);
        await nexus_bilding_api.infrastructure.persistence.Seeds.DefaultSeeds.SeedAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
    }
}

app.Run();
