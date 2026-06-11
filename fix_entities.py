import os
import re

def fix_file(filepath):
    with open(filepath, 'r') as f:
        lines = f.readlines()

    new_lines = []
    in_initializer = False
    for line in lines:
        if 'new ' in line and '{' in line:
            in_initializer = True
            new_lines.append(line)
            continue
        
        if in_initializer:
            if '};' in line:
                in_initializer = False
                new_lines.append(line)
                continue
            
            # Replace semicolon with comma at the end of the line (ignoring whitespace)
            fixed_line = re.sub(r';\s*$', ',\n', line)
            new_lines.append(fixed_line)
        else:
            new_lines.append(line)

    with open(filepath, 'w') as f:
        f.writelines(new_lines)

root_dir = '/home/fcastro_dev/Proyectos/Nexus_Bilding/src/Core/NexusBilling.Core.Domain'
for root, dirs, files in os.walk(root_dir):
    for file in files:
        if file.endswith('.cs'):
            fix_file(os.path.join(root, file))
