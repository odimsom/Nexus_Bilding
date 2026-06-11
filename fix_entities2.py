import os
import re

def fix_file(filepath):
    with open(filepath, 'r') as f:
        lines = f.readlines()

    new_lines = []
    for i, line in enumerate(lines):
        # Look for '},' that might have been '};'
        # specifically if the next line has 'return OperationResult'
        if line.strip() == '},' and i + 2 < len(lines):
            next_line = lines[i+2].strip()
            next_line2 = lines[i+1].strip()
            if next_line.startswith('return OperationResult') or next_line2.startswith('return OperationResult'):
                new_lines.append(line.replace('},', '};'))
                continue
        
        # Also fix any rogue '},' that is preceded by an object initialization block closing
        # This is a bit risky but mostly safe in this specific context where 'return' follows
        
        new_lines.append(line)

    with open(filepath, 'w') as f:
        f.writelines(new_lines)

root_dir = '/home/fcastro_dev/Proyectos/Nexus_Bilding/src/Core/NexusBilling.Core.Domain'
for root, dirs, files in os.walk(root_dir):
    for file in files:
        if file.endswith('.cs'):
            fix_file(os.path.join(root, file))
