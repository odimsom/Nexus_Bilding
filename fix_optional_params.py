import os
import re

def fix_file(filepath):
    with open(filepath, 'r') as f:
        content = f.read()

    # Find the Create method signature
    # It looks like: public static OperationResult<Entity, DomainError> Create( ... )
    
    # We can use a regex to match the Create method declaration
    pattern = r'(public static OperationResult<[^>]+>\s+Create\s*\()([^)]+)(\))'
    
    def replacer(match):
        prefix = match.group(1)
        params = match.group(2)
        suffix = match.group(3)
        
        # Remove = default or = null from parameters
        fixed_params = re.sub(r'\s*=\s*(?:default|null|string\.Empty)', '', params)
        
        return prefix + fixed_params + suffix

    new_content = re.sub(pattern, replacer, content)

    if new_content != content:
        with open(filepath, 'w') as f:
            f.write(new_content)

root_dir = '/home/fcastro_dev/Proyectos/Nexus_Bilding/src/Core/NexusBilling.Core.Domain'
for root, dirs, files in os.walk(root_dir):
    for file in files:
        if file.endswith('.cs'):
            fix_file(os.path.join(root, file))
