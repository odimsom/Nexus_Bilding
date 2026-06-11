import os
import re
from collections import defaultdict

solution_dir = "/home/fcastro_dev/Proyectos/Nexus_Bilding"
domain_dir = os.path.join(solution_dir, "src/Core/NexusBilling.Core.Domain")
infra_dir = os.path.join(solution_dir, "src/Infrastructure/NexusBilling.Infrastructure.Persistence")

def to_snake_case(name):
    s1 = re.sub('(.)([A-Z][a-z]+)', r'\1_\2', name)
    return re.sub('([a-z0-9])([A-Z])', r'\1_\2', s1).lower()

modules = ["Finance", "Sales", "Purchasing", "Inventory", "Security", "Administration"]

all_entities = []

for module in modules:
    entity_folder = os.path.join(domain_dir, module, "Entities")
    if not os.path.exists(entity_folder):
        continue
    for root, dirs, files in os.walk(entity_folder):
        for file in files:
            if file.endswith(".cs"):
                entity_name = file[:-3]
                file_path = os.path.join(root, file)
                all_entities.append((module, entity_name, file_path))

def get_properties(file_path):
    with open(file_path, "r", encoding='utf-8') as f:
        content = f.read()
    # match `public Type Name { get; private set; }`
    pattern = r"public\s+(?:virtual\s+)?([a-zA-Z0-9_\?\[\]<>]+)\s+([a-zA-Z0-9_]+)\s*\{"
    matches = re.findall(pattern, content)
    props = []
    for m in matches:
        prop_type = m[0]
        prop_name = m[1]
        if prop_name not in ["Id", "DomainEvents"]:
            props.append(prop_name)
    return props

def create_dir_if_not_exists(path):
    d = os.path.dirname(path)
    if not os.path.exists(d):
        os.makedirs(d)

for module, entity_name, file_path in all_entities:
    # Repo Interface
    repo_interface_path = os.path.join(domain_dir, module, "Repositories", f"I{entity_name}Repository.cs")
    if not os.path.exists(repo_interface_path):
        create_dir_if_not_exists(repo_interface_path)
        with open(repo_interface_path, "w", encoding='utf-8') as f:
            f.write(f"""using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.{module}.Entities;

namespace NexusBilling.Core.Domain.{module}.Repositories;

public interface I{entity_name}Repository : IGenericRepository<{entity_name}>
{{
}}
""")
            
    # Infra Repo
    infra_repo_path = os.path.join(infra_dir, module, "Repositories", f"{entity_name}Repository.cs")
    if not os.path.exists(infra_repo_path):
        create_dir_if_not_exists(infra_repo_path)
        with open(infra_repo_path, "w", encoding='utf-8') as f:
            f.write(f"""using NexusBilling.Core.Domain.{module}.Entities;
using NexusBilling.Core.Domain.{module}.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.{module}.Repositories;

public class {entity_name}Repository : GenericRepository<{entity_name}>, I{entity_name}Repository
{{
    public {entity_name}Repository(NexusBillingDbContext context) : base(context)
    {{
    }}
}}
""")

    # Configuration
    config_path = os.path.join(infra_dir, module, "Configurations", f"{entity_name}Configuration.cs")
    create_dir_if_not_exists(config_path)
    props = get_properties(file_path)
    
    with open(config_path, "w", encoding='utf-8') as f:
        f.write(f"""using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.{module}.Entities;

namespace NexusBilling.Infrastructure.Persistence.{module}.Configurations;

public class {entity_name}Configuration : IEntityTypeConfiguration<{entity_name}>
{{
    public void Configure(EntityTypeBuilder<{entity_name}> builder)
    {{
        builder.ToTable("{to_snake_case(entity_name)}", "{module.lower()}");
        builder.HasKey(x => x.Id);
""")
        for p in props:
            snake = to_snake_case(p)
            if p == "TenantId":
                f.write(f'        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("{snake}").IsRequired();\n')
            else:
                f.write(f'        builder.Property(x => x.{p}).HasColumnName("{snake}");\n')
        f.write("    }\n}\n")

# DbContext
db_context_path = os.path.join(infra_dir, "Context", "NexusBillingDbContext.cs")
mod_groups = defaultdict(list)
for module, entity_name, _ in all_entities:
    mod_groups[module].append(entity_name)

with open(db_context_path, "w", encoding='utf-8') as f:
    f.write("""using Microsoft.EntityFrameworkCore;
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
""")
    for module, entities in mod_groups.items():
        f.write(f"\n    // {module}\n")
        for e in entities:
            if e.endswith("y"): plural = e[:-1] + "ies"
            elif e.endswith("s"): plural = e + "es"
            else: plural = e + "s"
            if e == "ItemUnitOfMeasure": plural = "ItemUnitOfMeasures"
            f.write(f"    public DbSet<{e}> {plural} => Set<{e}>();\n")
    
    f.write("""
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
""")

# ServiceRegistration
sr_path = os.path.join(infra_dir, "ServiceRegistration.cs")
with open(sr_path, "w", encoding='utf-8') as f:
    f.write("""using Microsoft.Extensions.DependencyInjection;
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
""")
    for module, entities in mod_groups.items():
        f.write(f"\n        // {module}\n")
        for e in entities:
            f.write(f"        services.AddScoped<I{e}Repository, {e}Repository>();\n")
            
    f.write("""        
        return services;
    }
}
""")

print("Done.")
