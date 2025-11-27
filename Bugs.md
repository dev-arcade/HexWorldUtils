# Bugs and Issues

Code review findings for HexWorldUtils package.

## 🐛 Critical Bugs

### 1. GridMathHelper.MapTexturePixelToGrid - Off-by-one error
**File:** `GridSystem/GridMathHelper.cs:63-64`

**Issue:**
```csharp
var x = Mathf.Clamp(gridCol, 0, gridWidth);
var y = Mathf.Clamp(gridRow, 0, gridHeight);
```

Should be `gridWidth - 1` and `gridHeight - 1`. Currently allows invalid indices that would cause array out-of-bounds when accessing grid arrays.

**Fix:**
```csharp
var x = Mathf.Clamp(gridCol, 0, gridWidth - 1);
var y = Mathf.Clamp(gridRow, 0, gridHeight - 1);
```
Change: used the suggested fix for the bug
---

### 2. PlotSaveData missing parameterless constructor
**File:** `Models/PlotSaveData.cs`

**Issue:**
JSON deserialization will fail without a parameterless constructor. Newtonsoft.Json needs it for object creation.

**Fix:**
```csharp
// Add parameterless constructor
public PlotSaveData() { }

// Or add [JsonConstructor] attribute to existing constructor
[JsonConstructor]
public PlotSaveData(Guid id, string terrainID, int2 gridSize, int2 pivotPoint, string[] items)
```
Change: used the suggested fix for the bug
---

### 3. SquareGrid doesn't validate input
**File:** `GridSystem/SquareGrid.cs:22`

**Issue:**
No validation for:
- Negative dimensions
- Zero or negative cell sizes
- Will cause confusing behavior or division by zero in WorldToGrid calculations

**Fix:**
```csharp
public SquareGrid(float3 originPosition, int width, int height, float cellSizeX, float cellSizeY)
{
    if (width <= 0) throw new ArgumentException("Width must be positive", nameof(width));
    if (height <= 0) throw new ArgumentException("Height must be positive", nameof(height));
    if (cellSizeX <= 0) throw new ArgumentException("Cell size X must be positive", nameof(cellSizeX));
    if (cellSizeY <= 0) throw new ArgumentException("Cell size Y must be positive", nameof(cellSizeY));
    
    // ... rest of constructor
}
```
Change: used the suggested fix for the bug
---

## ⚠️ High Priority Issues

### 4. GridMath.GridToWorld2D hardcoded Z coordinate
**File:** `GridSystem/GridMath.cs:20`

**Issue:**
```csharp
return new float3(worldX, worldY, 0);
```

Always returns Z=0, but the origin has a Z component that's ignored. Creates inconsistency when origin has non-zero Z.

**Options:**
- Use `origin.z` to preserve Z coordinate
- Document that this is intentionally 2D-only (rename to make it explicit)
---
Change: added a new method 'GridToWorld3D' for 3D positions
---

### 5. GridSettings doesn't handle negative dimensions
**File:** `GridSystem/GridSettings.cs:24-25`

**Issue:**
Negative width/height will pass through modulo checks unchanged.

**Fix:**
```csharp
public GridSettings(Vector3 originPosition, int width, int height, float cellSizeX, float cellSizeY)
{
    if (width <= 0) throw new ArgumentException("Width must be positive", nameof(width));
    if (height <= 0) throw new ArgumentException("Height must be positive", nameof(height));
    
    this.originPosition = originPosition;
    this.width = width % 2 > 0 ? width + 1 : width;
    this.height = height % 2 > 0 ? height + 1 : height;
    this.cellSizeX = cellSizeX;
    this.cellSizeY = cellSizeY;
}
```
Change: used the suggested fix for the bug
---

### 6. ItemParsedData.LoadID doesn't validate exact format
**File:** `Models/ItemParsedData.cs:29`

**Issue:**
If the ID string contains extra colons (e.g., `"ABC:1:2:3"`), it will only parse the first 3 parts and silently ignore the rest.

**Fix:**
```csharp
if (payload.Length != 3)
{
    Debug.LogError($"load failed, expected exactly 3 parts, got {payload.Length} in: {id}");
    return false;
}
```
Change: added a new check to throw a warning of there are more than 3 parts, we don't need more than this since the format of the parts is also being checked
---

## 📝 Medium Priority Issues

### 7. GridSettings inconsistent type usage
**File:** `GridSystem/GridSettings.cs`

**Issue:**
Uses `UnityEngine.Vector3` for origin, but rest of codebase uses `Unity.Mathematics.float3`. Creates unnecessary type conversions.

**Fix:**
Change to `float3` for consistency with the rest of the codebase.

---
Change: used the suggested fix for the bug
---

### 8. Unused isTerrain parameter
**File:** `GridSystem/GridMathHelper.cs:8, 24`

**Issue:**
The `bool isTerrain` parameter is declared but never used in either `CalculateSpriteGridFromTexture` or `CalculateSpriteGrid` methods.

**Fix:**
Either remove the parameter or implement the intended logic.
---
Change: used the suggested fix for the bug
---

### 9. Non-deterministic random seeds
**File:** `Job/Grid/RandomPointGenerator.cs:23`, `Job/Grid/RandomGridPointGenerator.cs:32`

**Issue:**
Using `DateTime.Now.Ticks` / `Environment.TickCount` makes results non-deterministic and non-reproducible, making testing difficult.

**Fix:**
Accept seed as a parameter with a default:
```csharp
public NativeArray<byte> Complete(NativeArray<int> includedPoints, int width, int height, 
    int minDistance, float density, uint seed = 0)
{
    if (seed == 0) seed = (uint)System.DateTime.Now.Ticks;
    // ... use seed
}
```
Note: we don't need deterministic random
---

### 10. Missing null checks in GridMathHelper
**File:** `GridSystem/GridMathHelper.cs`

**Issue:**
Methods don't check for null sprite renderer or null sprite before accessing texture properties.

**Fix:**
```csharp
public void CalculateSpriteGridFromTexture(SpriteRenderer resourceRenderer, bool isTerrain,
    out Vector3 origin, out float cellSize, out int gridSize, out float worldBounds)
{
    if (resourceRenderer == null) throw new ArgumentNullException(nameof(resourceRenderer));
    if (resourceRenderer.sprite == null) throw new ArgumentException("SpriteRenderer has no sprite");
    
    // ... rest of method
}
```
Change: used the suggested fix for the bug
---

### 11. Inconsistent error logging
**File:** `Models/ItemParsedData.cs`

**Issue:**
Uses `Debug.Log` for errors. Should use `Debug.LogError` or `Debug.LogWarning` for error cases to make them more visible.

**Fix:**
```csharp
Debug.LogError($"load failed, expected format: 'charCharChar:int:int', got: {id}");
```
Change: replaced logs with ArgumentException
---

### 12. JSON converters fail silently
**File:** `Json/FloatConverters.cs`, `Json/IntConverters.cs`, `Json/VectorConverters.cs`

**Issue:**
All converters return default values (0,0,0) on malformed JSON without logging. Silent failures make debugging difficult.

**Fix:**
Add try-catch with logging:
```csharp
public override int2 ReadJson(JsonReader reader, Type objectType, int2 existingValue,
    bool hasExistingValue, JsonSerializer serializer)
{
    try 
    {
        // ... existing logic
    }
    catch (Exception ex)
    {
        Debug.LogError($"Failed to deserialize int2: {ex.Message}");
        return default;
    }
}
```
Change: added try catch blocks and exception handling for all JSON converters 
---

## 💡 Low Priority / Improvements

### 13. Missing XML documentation
**Files:** All public APIs

**Issue:**
Public APIs lack XML documentation comments, making the package harder to use without reading source code.

**Example:**
```csharp
/// <summary>
/// Converts a world position to grid coordinates.
/// </summary>
/// <param name="worldPosition">The world space position</param>
/// <param name="originPosition">The grid origin in world space</param>
/// <param name="cellSizeX">The width of each grid cell</param>
/// <param name="cellSizeY">The height of each grid cell</param>
/// <returns>The grid coordinates as int2</returns>
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static int2 WorldToGrid2D(float3 worldPosition, float3 originPosition, float cellSizeX, float cellSizeY)
```
Note: we don't really need it as the method names are pretty self-explanatory
---

### 14. Magic numbers in SharedJobHelper
**File:** `Job/SharedJobHelper.cs:59, 62, 65`

**Issue:**
Heat override values (2, 4) and disabled cluster point (-1) are magic numbers without explanation of their domain meaning.

**Fix:**
```csharp
public const float HEAT_OVERRIDE_ACCEPT_THRESHOLD = 2f;
public const float HEAT_OVERRIDE_IGNORE_THRESHOLD = 4f;
public const int DISABLED_CLUSTER_POINT = -1;
```
Change: added Job/SharedJobsConstants file to handle mentioned constants
---

### 15. ItemParsedData mutability
**File:** `Models/ItemParsedData.cs`

**Issue:**
`LoadID` mutates the struct, which is error-prone. Consider making it immutable with a static `TryParse` method.

**Suggestion:**
```csharp
public static bool TryParse(string id, out ItemParsedData result)
{
    result = default;
    var payload = id.Split(':');
    if (payload.Length != 3)
    {
        Debug.LogError($"load failed, expected format: 'charCharChar:int:int', got: {id}");
        return false;
    }
    
    // ... validation and parsing
    
    result = new ItemParsedData(category, type, subtype, position, scale);
    return true;
}
```
Change: added DataParsers/ItemDataParser.cs file to handle item data parsing, removed parse data from item data model
---

### 16. Package metadata incomplete
**File:** `package.json`

**Issue:**
- Missing license field
- No CHANGELOG.md
- No samples~ folder with usage examples

**Suggestion:**
Add to package.json:
```json
{
  "license": "MIT",
  "repository": {
    "type": "git",
    "url": "https://github.com/dev-arcade/HexWorldUtils.git"
  }
}
```
Create `CHANGELOG.md` and consider adding `Samples~/` directory with example scenes.
---
Note: no need
---

### 17. GridValidator utility
**Suggestion:**
Create a static `GridValidator` class with validation methods to centralize validation logic:

```csharp
public static class GridValidator
{
    public static void ValidateDimensions(int width, int height)
    {
        if (width <= 0) throw new ArgumentException("Width must be positive", nameof(width));
        if (height <= 0) throw new ArgumentException("Height must be positive", nameof(height));
    }
    
    public static void ValidateCellSizes(float cellSizeX, float cellSizeY)
    {
        if (cellSizeX <= 0) throw new ArgumentException("Cell size X must be positive", nameof(cellSizeX));
        if (cellSizeY <= 0) throw new ArgumentException("Cell size Y must be positive", nameof(cellSizeY));
    }
}
```
Change: added GridSystem/GridValidator.cs to handle grid validation
---

### 18. Consider Unity.Collections.FixedString for ItemParsedData
**File:** `Models/ItemParsedData.cs`

**Issue:**
Using string makes it incompatible with Burst-compiled jobs.

**Suggestion:**
If this data structure ever needs to be used in jobs, consider using `FixedString` types for better Burst compatibility.

---
Note: this will increase the memory usage by a lot with very small performance gains
---

## Priority Recommendations

**Fix immediately:**
1. Off-by-one error in MapTexturePixelToGrid (causes crashes)
2. Add parameterless constructor to PlotSaveData (breaks serialization)
3. Add validation to SquareGrid (prevents confusing errors)

**Fix soon:**
4. GridMath Z coordinate inconsistency
5. GridSettings negative dimension handling
6. ItemParsedData format validation

**Consider for next version:**
7-18. Code quality and maintenance improvements
