import re
import os

sql_file = '/home/fcastro_dev/dev_directories/projector_importantes_para_el_futuro/bcentral-nav2017-schema/db-erp-funcional-2026/ddl/general_ledger.sql'
base_dir = '/home/fcastro_dev/Proyectos/Nexus_Bilding/src'

domain_entities_dir = os.path.join(base_dir, 'Core/NexusBilling.Core.Domain/Finance/Entities')
domain_repos_dir = os.path.join(base_dir, 'Core/NexusBilling.Core.Domain/Finance/Repositories')
persist_configs_dir = os.path.join(base_dir, 'Infrastructure/NexusBilling.Infrastructure.Persistence/Finance/Configurations')
persist_repos_dir = os.path.join(base_dir, 'Infrastructure/NexusBilling.Infrastructure.Persistence/Finance/Repositories')

os.makedirs(domain_entities_dir, exist_ok=True)
os.makedirs(domain_repos_dir, exist_ok=True)
os.makedirs(persist_configs_dir, exist_ok=True)
os.makedirs(persist_repos_dir, exist_ok=True)

with open(sql_file, 'r', encoding='utf-8') as f:
    sql = f.read()

def to_pascal_case(snake_str):
    components = snake_str.split('_')
    return "".join(x.title() for x in components)

def to_camel_case(snake_str):
    components = snake_str.split('_')
    return components[0] + "".join(x.title() for x in components[1:])

type_mapping = {
    'bigint': 'long',
    'uuid': 'Guid',
    'date': 'DateTime',
    'timestamptz': 'DateTime',
    'varchar': 'string',
    'boolean': 'bool',
    'smallint': 'short',
    'integer': 'int',
    'numeric': 'decimal',
    'bytea': 'byte[]'
}

tables = re.findall(r'create table erp\.(\w+)\s*\((.*?)\);', sql, re.DOTALL)

db_sets = []
registrations = []

for table_name, columns_str in tables:
    class_name = to_pascal_case(table_name)
    class_name = class_name.replace('GL', 'GL') # e.g. GLEntry
    # wait, g_l_account -> GLAccount
    if class_name.startswith('GL'):
        pass # It will be GLAccount instead of GLAccount if snake_str is g_l_account -> G_l_account -> GLAccount.
        # Actually g_l_account -> G L Account -> GLAccount.
    
    # Custom fix for G_L prefixes
    class_name = class_name.replace('GL', 'GL') # already G L -> GL
    
    columns = []
    lines = columns_str.split('\n')
    for line in lines:
        line = line.split('--')[0].strip()
        if not line: continue
        if line.startswith('unique'): continue
        if line.startswith('primary key'): continue
        
        parts = line.split()
        if not parts: continue
        
        col_name = parts[0]
        col_type = parts[1].split('(')[0]
        
        is_not_null = 'not null' in line.lower()
        is_primary_key = 'primary key' in line.lower()
        
        csharp_type = type_mapping.get(col_type, 'string')
        
        if not is_not_null and csharp_type != 'string' and csharp_type != 'byte[]':
            csharp_type += '?'
            
        columns.append({
            'name': col_name,
            'prop_name': to_pascal_case(col_name),
            'type': csharp_type,
            'is_not_null': is_not_null,
            'is_pk': is_primary_key
        })
        
    # Generate Entity
    entity_code = f"""using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class {class_name} : Entity
{{
    private {class_name}() {{ }}

"""
    # Properties
    for col in columns:
        if col['name'] in ['id', 'created_at', 'updated_at']: continue
        if col['name'] == 'tenant_id':
            entity_code += f"    public TenantIdentifier TenantId {{ get; private set; }}\n"
        else:
            prop_type = col['type']
            if prop_type == 'string' and not col['is_not_null']:
                prop_type = 'string?'
            entity_code += f"    public {prop_type} {col['prop_name']} {{ get; private set; }}\n"
            
    # Create method
    mandatory_args = []
    assignments = []
    validations = []
    
    for col in columns:
        if col['name'] in ['id', 'created_at', 'updated_at']: continue
        arg_name = to_camel_case(col['name'])
        
        if col['name'] == 'tenant_id':
            mandatory_args.append("TenantIdentifier tenantId")
            assignments.append("TenantId = tenantId;")
            validations.append(f"""        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<{class_name}, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));""")
        else:
            prop_type = col['type']
            arg_type = prop_type
            if col['is_not_null']:
                mandatory_args.append(f"{arg_type} {arg_name}")
                if prop_type == 'string':
                    validations.append(f"""        if (string.IsNullOrWhiteSpace({arg_name}))
            return OperationResult<{class_name}, DomainError>.Fail(DomainError.Validation("finance.{col['name']}_required", "El campo {col['name']} es obligatorio."));""")
                    assignments.append(f"{col['prop_name']} = {arg_name}.Trim();")
                else:
                    assignments.append(f"{col['prop_name']} = {arg_name};")
            else:
                if prop_type == 'string': arg_type = 'string?'
                mandatory_args.append(f"{arg_type} {arg_name} = default")
                assignments.append(f"{col['prop_name']} = {arg_name};")

    args_str = ",\n        ".join(mandatory_args)
    validations_str = "\n".join(validations)
    assignments_str = "\n            ".join(assignments)

    entity_code += f"""
    public static OperationResult<{class_name}, DomainError> Create(
        {args_str})
    {{
{validations_str}

        var entity = new {class_name}()
        {{
            {assignments_str}
        }};

        return OperationResult<{class_name}, DomainError>.Ok(entity);
    }}
}}
"""
    with open(os.path.join(domain_entities_dir, f"{class_name}.cs"), 'w') as f:
        f.write(entity_code)
        
    # Generate IRepository
    repo_interface_code = f"""using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface I{class_name}Repository
{{
    Task<{class_name}?> GetByIdAsync(long id);
    Task<IEnumerable<{class_name}>> GetAllAsync();
    Task AddAsync({class_name} entity);
    Task UpdateAsync({class_name} entity);
    Task DeleteAsync({class_name} entity);
}}
"""
    with open(os.path.join(domain_repos_dir, f"I{class_name}Repository.cs"), 'w') as f:
        f.write(repo_interface_code)
        
    # Generate Configuration
    config_code = f"""using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class {class_name}Configuration : IEntityTypeConfiguration<{class_name}>
{{
    public void Configure(EntityTypeBuilder<{class_name}> builder)
    {{
        builder.ToTable("{table_name}", "finance");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();
            
"""
    for col in columns:
        if col['name'] in ['id', 'created_at', 'updated_at']: continue
        if col['name'] == 'tenant_id':
            config_code += f"""        builder.Property(x => x.TenantId)
            .HasColumnName("tenant_id")
            .HasConversion(v => v.Value, v => TenantIdentifier.Create(v))
            .IsRequired();\n"""
        else:
            req = ".IsRequired()" if col['is_not_null'] else ""
            config_code += f"""        builder.Property(x => x.{col['prop_name']})
            .HasColumnName("{col['name']}"){req};\n"""
            
    config_code += f"""
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }}
}}
"""
    with open(os.path.join(persist_configs_dir, f"{class_name}Configuration.cs"), 'w') as f:
        f.write(config_code)
        
    # Generate Repository
    repo_code = f"""using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class {class_name}Repository : I{class_name}Repository
{{
    private readonly NexusBillingDbContext _context;

    public {class_name}Repository(NexusBillingDbContext context)
    {{
        _context = context;
    }}

    public async Task<{class_name}?> GetByIdAsync(long id)
    {{
        return await _context.Set<{class_name}>().FindAsync(id);
    }}

    public async Task<IEnumerable<{class_name}>> GetAllAsync()
    {{
        return await _context.Set<{class_name}>().ToListAsync();
    }}

    public async Task AddAsync({class_name} entity)
    {{
        await _context.Set<{class_name}>().AddAsync(entity);
    }}

    public Task UpdateAsync({class_name} entity)
    {{
        _context.Set<{class_name}>().Update(entity);
        return Task.CompletedTask;
    }}

    public Task DeleteAsync({class_name} entity)
    {{
        _context.Set<{class_name}>().Remove(entity);
        return Task.CompletedTask;
    }}
}}
"""
    with open(os.path.join(persist_repos_dir, f"{class_name}Repository.cs"), 'w') as f:
        f.write(repo_code)

    db_sets.append(f"    public DbSet<{class_name}> {class_name}s => Set<{class_name}>();")
    registrations.append(f"        services.AddScoped<I{class_name}Repository, {class_name}Repository>();")

print("DBSETS:")
print("\n".join(db_sets))
print("REGISTRATIONS:")
print("\n".join(registrations))
