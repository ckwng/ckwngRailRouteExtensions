import os
import zipfile

target_path = ['bin', 'Release']
harmony_label = '0Harmony'
harmony_dll_filename = f'{harmony_label}.dll'
harmony_mod_folder_name = f'{harmony_label}'
extensions_label = 'ckwngRailRouteExtensions'
extensions_mod_folder_name = f'{extensions_label}'
extensions_dll_filename = f'{extensions_label}.dll'
artifact_filename = f'{extensions_label}.zip'

with zipfile.ZipFile(os.path.join(*target_path, artifact_filename), 'w') as artifact:
	artifact.write(os.path.join(*target_path, extensions_dll_filename), 
		os.path.join(extensions_mod_folder_name, extensions_dll_filename))
	artifact.write(os.path.join(*target_path, harmony_dll_filename), 
		os.path.join(harmony_mod_folder_name, harmony_dll_filename))
