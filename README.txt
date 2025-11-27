How to install:
Open package manager > click on + > 'Install package from disk...' > select the 'package.json' file in this folder


Notes:
- If the project uses assembly an definition, add the 'MoonPlotsCartographerSharedAssemblyDef.asmdef' file in this folder to the assembly definition of the project.
- If the assembly definition of the package is broken or not working (and if you need to have one, it's optional), you'll need to create a new assembly definition asset, enable 'Allow unsafe Code' option and add the following definitions:
	- Unity.Mathematics
	- Unity.Collections
	- Unity.Burst