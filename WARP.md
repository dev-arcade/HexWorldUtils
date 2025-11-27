# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview
HexWorldUtils is a Unity package containing shared logic and data models for "Moon Plots" and "Cartographer" projects. This is a Unity 6000.0+ package (`com.tothemoon.hex-world-utils`) designed to handle grid-based world systems, spatial calculations, and data serialization.

## Installation & Setup
This package is installed via Unity Package Manager using "Install package from disk..." and selecting `package.json` in this folder.

**Assembly Definition Requirements:**
- The package requires `allowUnsafeCode: true`
- Required assembly references (GUIDs in HexWorldUtils.asmdef):
  - Unity.Mathematics (`d8b63aba1907145bea998dd612889d6b`)
  - Unity.Collections (`e0cd26848372d4e5c891c569017e11f1`)
  - Unity.Burst (`2665a8d13d1b3f18800f46e256720795`)

## Architecture

### Core Systems

#### GridSystem (GridSystem/)
Handles 2D square grid generation and world/grid coordinate conversions.

**Key Components:**
- `GridMath`: Core coordinate conversion utilities (world ↔ grid)
  - Uses `float3` for world positions, `int2` for grid addresses
  - All conversions use QR axial coordinates exclusively (user preference)
- `SquareGrid`: Main grid data structure with parallel job-based generation
  - Stores pre-calculated world positions for each grid cell in a 2D array
  - Grid dimensions are forced to even numbers
- `GridHandler`: MonoBehaviour for runtime grid management with event callbacks
- `GridMathHelper`: Sprite-based grid calculations for texture mapping
- `GridRenderer`: Editor visualization with Gizmos (wire cubes, sphere bounds, address labels)

#### Job System (Job/)
Burst-compiled parallel jobs for high-performance computations.

**Grid Jobs (Job/Grid/):**
- `GridCalculator`: Parallel grid position generation using `IJobFor`
- `RandomPointGenerator`: Spatial hash-based Poisson disk sampling with density control
  - Implements shuffle + parallel processing pipeline
  - Uses atomic operations for thread-safe hash table updates
- `RandomGridPointGenerator`: Random point distribution with minimum distance constraints

**Shared Utilities (Job/):**
- `SharedJobHelper`: Thread-safe atomic operations, batch size calculation, grid indexing
  - Contains domain-specific "heat override" and "cluster point" helper methods
  - Provides hash functions for deterministic randomization

**General Jobs (Job/General/):**
- `ArrayFillerJob<T>`: Generic parallel array copying utility

#### Models (Models/)
Data structures for serialization.

- `PlotSaveData`: Plot persistence with terrain ID, grid size, pivot point, and item encoding
  - Items format: `"{Category}{Type}{Subtype}:{Position}:{Scale}"`
- `ItemParsedData`: Item data parser/serializer with validation

#### JSON Serialization (Json/)
Custom Newtonsoft.Json converters for Unity and Unity.Mathematics types.

- `JsonSettingsRef`: Centralized serializer settings with all converters registered
- Converters provided:
  - Unity types: `Vector2`, `Vector3`, `Vector2Int`, `Vector3Int`
  - Unity.Mathematics types: `float2`, `float3`, `int2`, `int3`

### Coordinate System
**CRITICAL:** This codebase uses QR axial coordinates exclusively. All UV coordinate references should be purged when encountered.

### Performance Patterns
- All grid jobs use `SharedJobHelper.GetBatchCount()` for optimal worker thread utilization
- Jobs leverage `[BurstCompile]` and `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for performance
- Unsafe code patterns for atomic operations in parallel contexts
- Grid data stored as flat `NativeArray` during jobs, converted to 2D arrays after completion

## Code Patterns

### Grid Usage Pattern
```csharp
var settings = new GridSettings(origin, width, height, cellSizeX, cellSizeY);
var grid = new SquareGrid(settings.OriginPosition, settings.Width, settings.Height, 
    settings.CellSizeX, settings.CellSizeY);

// World to grid
int2 gridPos = grid.WorldToGrid(worldPosition);

// Grid to world
float3 worldPos = grid.GridToWorld(gridPos);

// Bounds checking
bool isValid = grid.IsInsideGrid(gridPos);
```

### JSON Serialization Pattern
```csharp
using HexWorldUtils.Json;

string json = JsonConvert.SerializeObject(data, JsonSettingsRef.DefaultSettings);
var data = JsonConvert.DeserializeObject<T>(json, JsonSettingsRef.DefaultSettings);
```

### Job Runner Pattern
All job runners follow the pattern:
```csharp
public class JobRunner
{
    public ResultType Complete(params, JobHandle dependency = default)
    {
        // 1. Allocate NativeArrays with Allocator.TempJob
        // 2. Schedule job with SharedJobHelper.GetBatchCount()
        // 3. Complete job
        // 4. Extract results
        // 5. Dispose NativeArrays
        return results;
    }
}
```

## Common Pitfalls
- Grid dimensions are automatically rounded to even numbers in `GridSettings`
- `RandomPointGenerator` requires pre-shuffled indices; uses two-stage pipeline (shuffle → process)
- All jobs must dispose their `NativeArray` allocations
- `GridRenderer` requires `UnityEditor` and only works in editor (uses `Handles.Label`)
- Atomic operations in jobs require `[NativeDisableParallelForRestriction]` on shared arrays

## Dependencies
- Unity 6000.0+
- Unity.Mathematics
- Unity.Collections
- Unity.Burst
- Newtonsoft.Json (Json.NET for Unity)
- UnityEditor (GridRenderer only)
