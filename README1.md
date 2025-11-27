Info:
This package contains shared logic and data models between Moon Plots and Cartographer project.


How to install:

Method 1 - Git URL (Recommended):
Open Package Manager > click on + > 'Add package from git URL...' > paste the repository URL:
https://github.com/dev-arcade/HexWorldUtils.git

Method 2 - Local disk:
Open Package Manager > click on + > 'Install package from disk...' > select the 'package.json' file in this folder


Notes:
- If the project uses an assembly definition, add the assembly definition asset in this folder to the assembly definition of the project.
- If the assembly definition of the package is broken or not working (and if you need to have one, it's optional), you'll need to create a new assembly definition asset, enable 'Allow unsafe Code' option and add the following definitions:
	- Unity.Mathematics
	- Unity.Collections
	- Unity.Burst
    - Total Json
