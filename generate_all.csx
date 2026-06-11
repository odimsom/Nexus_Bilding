using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

string solutionDir = "./Proyectos/Nexus_Bilding";
string domainDir = Path.Combine(solutionDir, "src/Core/NexusBilling.Core.Domain");
string infraDir = Path.Combine(solutionDir, "src/Infrastructure/NexusBilling.Infrastructure.Persistence");

string ToSnakeCase(string input)
{
    if (string.IsNullOrEmpty(input)) { return input; }
    var startUnderscores = Regex.Match(input, @"^_+");
    return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
}

string[] modules = new[] { "Finance", "Sales", "Purchasing", "Inventory", "Security", "Administration" };

var allEntities = new List<(string Module, string EntityName, string Path)>();
var allRepos = new List<(string Module, string EntityName)>();

foreach (var module in modules)
{
    var entityFolder = Path.Combine(domainDir, module, "Entities");
    if (!Directory.Exists(entityFolder)) continue;

    foreach (var file in Directory.GetFiles(entityFolder, "*.cs"))
    {
        string entityName = Path.GetFileNameWithoutExtension(file);
        allEntities.Add((module, entityName, file));
    }
}

// Helper for property extraction
List<string> GetProperties(string filePath)
{
    var lines = File.ReadAllLines(filePath);
    var props = new List<string>();
    foreach (var line in lines)
    {
        var match = Regex.Match(line, @"public\s+(?:virtual\s+)?([a-zA-Z0-9_\?\[\]<>]+)\s+([a-zA-Z0-9_]+)\s*\{");
        if (match.Success)
        {
            var propName = match.Groups[2].Value;
            if (propName != "Id" && propName != "DomainEvents")
            {
                props.Add(propName);
            }
        }
    }
    return props;
}

foreach (var entity in allEntities)
{
    // Generate Domain Repo Interface
    string repoInterfacePath = Path.Combine(domainDir, entity.Module, "Repositories", $"I{entity.EntityName}Repository.cs");
    if (!File.Exists(repoInterfacePath))
    {
        Directory.CreateDirectory(Path.GetDirectoryName(repoInterfacePath));
        File.WriteAllText(repoInterfacePath, 
$@"using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.{entity.Module}.Entities;

namespace NexusBilling.Core.Domain.{entity.Module}.Repositories;

public interface I{entity.EntityName}Repository : IGenericRepository<{entity.EntityName}>
{{
}}
");
    }
    allRepos.Add((entity.Module, entity.EntityName));

    // Generate Infra Repo
    string infraRepoPath = Path.Combine(infraDir, entity.Module, "Repositories", $"{entity.EntityName}Repository.cs");
    if (!File.Exists(infraRepoPath))
    {
        Directory.CreateDirectory(Path.GetDirectoryName(infraRepoPath));
        File.WriteAllText(infraRepoPath, 
$@"using NexusBilling.Core.Domain.{entity.Module}.Entities;
using NexusBilling.Core.Domain.{entity.Module}.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.{entity.Module}.Repositories;

public class {entity.EntityName}Repository : GenericRepository<{entity.EntityName}>, I{entity.EntityName}Repository
{{
    public {entity.EntityName}Repository(NexusBillingDbContext context) : base(context)
    {{
    }}
}}
");
    }

    // Generate Configuration OVERWRITING ALL to ensure 100% field mapping
    string configPath = Path.Combine(infraDir, entity.Module, "Configurations", $"{entity.EntityName}Configuration.cs");
    var props = GetProperties(entity.Path);
    var sb = new StringBuilder();
    sb.AppendLine($@"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.{entity.Module}.Entities;

namespace NexusBilling.Infrastructure.Persistence.{entity.Module}.Configurations;

public class {entity.EntityName}Configuration : IEntityTypeConfiguration<{entity.EntityName}>
{{
    public void Configure(EntityTypeBuilder<{entity.EntityName}> builder)
    {{
        builder.ToTable(""{ToSnakeCase(entity.EntityName)}"", ""{entity.Module.ToLower()}"");
        builder.HasKey(x => x.Id);
");

    foreach(var p in props)
    {
        string snake = ToSnakeCase(p);
        if (p == "TenantId") {
            sb.AppendLine($"        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName(\"{snake}\").IsRequired();");
        } else {
            sb.AppendLine($"        builder.Property(x => x.{p}).HasColumnName(\"{snake}\");");
        }
    }
    
    sb.AppendLine("    }");
    sb.AppendLine("}");

    Directory.CreateDirectory(Path.GetDirectoryName(configPath));
    File.WriteAllText(configPath, sb.ToString());
}

// Update DbContext
string dbContextPath = Path.Combine(infraDir, "Context", "NexusBillingDbContext.cs");
var dbContextSb = new StringBuilder();
dbContextSb.AppendLine(@"using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Purchasing.Entities;
using System.Reflection;

namespace NexusBilling.Infrastructure.Persistence.Context;

public class NexusBillingDbContext : DbContext
{
    public NexusBillingDbContext(DbContextOptions<NexusBillingDbContext> options) : base(options)
    {
    }
");
foreach (var modGroup in allEntities.GroupBy(x => x.Module))
{
    dbContextSb.AppendLine($"    // {modGroup.Key}");
    foreach (var e in modGroup)
    {
        string plural = e.EntityName.EndsWith("y") ? e.EntityName.Substring(0, e.EntityName.Length - 1) + "ies" : 
                        e.EntityName.EndsWith("s") ? e.EntityName + "es" :
                        e.EntityName + "s";
        if (e.EntityName == "ItemUnitOfMeasure") plural = "ItemUnitOfMeasures"; // hack
        dbContextSb.AppendLine($"    public DbSet<{e.EntityName}> {plural} => Set<{e.EntityName}>();");
    }
}
dbContextSb.AppendLine(@"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}");
File.WriteAllText(dbContextPath, dbContextSb.ToString());


// Update ServiceRegistration
string srPath = Path.Combine(infraDir, "ServiceRegistration.cs");
var srSb = new StringBuilder();
srSb.AppendLine(@"using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;
using NexusBilling.Infrastructure.Persistence.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Security.Repositories;

namespace NexusBilling.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
");
foreach (var modGroup in allEntities.GroupBy(x => x.Module))
{
    srSb.AppendLine($"        // {modGroup.Key}");
    foreach (var e in modGroup)
    {
        srSb.AppendLine($"        services.AddScoped<I{e.EntityName}Repository, {e.EntityName}Repository>();");
    }
}
srSb.AppendLine(@"        
        return services;
    }
}");
File.WriteAllText(srPath, srSb.ToString());

Console.WriteLine("Done.");