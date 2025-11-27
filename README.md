# Hex World Utils

[![Unity](https://img.shields.io/badge/Unity-6000.0%2B-black?logo=unity)](https://unity.com/)
[![License](https://img.shields.io/badge/License-Proprietary-red.svg)](#-license)
[![GitHub](https://img.shields.io/badge/GitHub-dev--arcade%2FHexWorldUtils-181717?logo=github)](https://github.com/dev-arcade/HexWorldUtils)

A high-performance Unity package for grid-based world systems, spatial calculations, and procedural generation. Built with Unity DOTS (Burst, Jobs, Collections) for maximum performance.

> **Shared Package**: This package contains shared logic and data models between the **Moon Plots** and **Cartographer** projects.

---

## ✨ Features

### 🔷 Grid Systems
- **Square Grid Generation** with parallel job-based calculation
- **World ↔ Grid** coordinate conversion utilities
- **QR Axial Coordinates** support (user-preferred coordinate system)
- Customizable cell sizes and grid dimensions
- Grid bounds checking and validation
- Sprite-based grid calculations for texture mapping

### ⚡ High-Performance Jobs
- **Burst-compiled** parallel jobs for maximum performance
- **Spatial Hash** algorithms for efficient point distribution
- **Poisson Disk Sampling** for organic point generation
- **Random Point Generation** with density and distance controls
- Thread-safe atomic operations for parallel processing
- Optimized batch sizing for worker thread utilization

### 💾 Data Management
- **JSON Serialization** with custom converters for Unity types
- Save/Load system for plot and terrain data
- Support for `Unity.Mathematics` types (`float2`, `float3`, `int2`, etc.)
- Item encoding/decoding with validation
- GUID-based plot identification

### 🎨 Visualization & Debug
- **Grid Renderer** with Gizmos visualization
- Wire cube and sphere rendering modes
- Grid address labels in editor
- Bounds visualization tools
- Customizable grid colors

---

## 📦 Installation

### Method 1: Git URL (Recommended)

1. Open **Unity Package Manager** (`Window > Package Manager`)
2. Click the **+** button
3. Select **Add package from git URL...**
4. Paste the URL:
   ```
   https://github.com/dev-arcade/HexWorldUtils.git
   ```

### Method 2: Local Installation

1. Clone or download this repository
2. Open **Unity Package Manager** (`Window > Package Manager`)
3. Click the **+** button
4. Select **Install package from disk...**
5. Navigate to the cloned folder and select `package.json`

---

## 🚀 Quick Start

### Basic Grid Generation

```csharp
using HexWorldUtils.GridSystem;
using Unity.Mathematics;

// Create grid settings
var settings = new GridSettings(
    originPosition: new Vector3(0, 0, 0),
    width: 64,
    height: 64,
    cellSizeX: 1.0f,
    cellSizeY: 1.0f
);

// Generate grid
var grid = new SquareGrid(
    settings.OriginPosition,
    settings.Width,
    settings.Height,
    settings.CellSizeX,
    settings.CellSizeY
);

// Convert world position to grid coordinates
int2 gridPos = grid.WorldToGrid(worldPosition);

// Convert grid coordinates back to world position
float3 worldPos = grid.GridToWorld(gridPos);

// Check if position is inside grid
bool isValid = grid.IsInsideGrid(gridPos);
```

### Using GridHandler MonoBehaviour

```csharp
using HexWorldUtils.GridSystem;
using UnityEngine;

public class MyGridController : MonoBehaviour
{
    [SerializeField] private GridHandler gridHandler;
    
    private void Start()
    {
        // Subscribe to grid generation event
        gridHandler.OnGenerated += OnGridGenerated;
    }
    
    private void OnGridGenerated(float3[,] gridData)
    {
        Debug.Log($"Grid generated with {gridData.Length} cells");
        // Use grid data...
    }
}
```

### Random Point Distribution

```csharp
using HexWorldUtils.Job.Grid;
using Unity.Collections;

var runner = new RandomGridPointGeneratorRunner();

// Generate random points with minimum distance and density
NativeArray<float2> points = runner.Complete(
    width: 100,
    height: 100,
    minDistance: 5,
    density: 0.8f,
    targetWidth: 100,
    targetHeight: 100
);

// Use points...

// Don't forget to dispose!
points.Dispose();
```

### JSON Serialization

```csharp
using HexWorldUtils.Json;
using HexWorldUtils.Models;
using Newtonsoft.Json;
using Unity.Mathematics;

// Create plot data
var plotData = new PlotSaveData(
    id: System.Guid.NewGuid(),
    terrainID: "forest_01",
    gridSize: new int2(64, 64),
    pivotPoint: new int2(0, 0),
    items: new string[] { "ABC:123:1", "DEF:456:2" }
);

// Serialize with custom converters
string json = JsonConvert.SerializeObject(
    plotData,
    JsonSettingsRef.DefaultSettings
);

// Deserialize
var loaded = JsonConvert.DeserializeObject<PlotSaveData>(
    json,
    JsonSettingsRef.DefaultSettings
);
```

---

## 📚 Core Components

### GridSystem
| Class | Description |
|-------|-------------|
| `GridMath` | Static utility for coordinate conversions |
| `SquareGrid` | Main grid data structure with job-based generation |
| `GridSettings` | Serializable grid configuration |
| `GridHandler` | MonoBehaviour for runtime grid management |
| `GridMathHelper` | Sprite-based grid calculations |
| `GridRenderer` | Gizmos-based grid visualization |

### Job System
| Class | Description |
|-------|-------------|
| `GridCalculatorRunner` | Parallel grid position generation |
| `RandomPointGeneratorRunner` | Poisson disk sampling with density control |
| `RandomGridPointGeneratorRunner` | Random point distribution |
| `SharedJobHelper` | Thread-safe utilities and batch calculations |
| `ArrayFillerJob<T>` | Generic parallel array operations |

### Models & Serialization
| Class | Description |
|-------|-------------|
| `PlotSaveData` | Plot persistence with grid metadata |
| `ItemParsedData` | Item data parser with validation |
| `JsonSettingsRef` | Centralized JSON settings with custom converters |

---

## 🔧 Architecture

### Performance Patterns
- **Job-based parallelism**: All grid operations use Unity Jobs for multi-threading
- **Burst compilation**: Critical paths optimized with `[BurstCompile]`
- **Aggressive inlining**: Hot paths marked with `[MethodImpl(AggressiveInlining)]`
- **Optimal batching**: Worker thread count calculation for efficient scheduling
- **Memory efficiency**: NativeArrays with `Allocator.TempJob` for temporary allocations

### Coordinate Systems
- **World Space**: `float3` positions in Unity world coordinates
- **Grid Space**: `int2` discrete grid cell addresses
- **QR Axial**: User-preferred hex coordinate system (future-ready)

### Threading & Safety
- Atomic operations for thread-safe hash table updates
- `[NativeDisableParallelForRestriction]` for shared array access
- Lock-free algorithms using `Interlocked` operations

---

## ⚙️ Requirements

- **Unity 6000.0+**
- **Dependencies:**
  - Unity.Mathematics
  - Unity.Collections
  - Unity.Burst
  - Newtonsoft.Json (Json.NET for Unity)
  - UnityEditor (for visualization tools)

### Assembly Definition
The package requires `allowUnsafeCode: true` for atomic operations. Assembly references are automatically configured.

---

## 📖 Documentation

- **[WARP.md](WARP.md)** - Architecture and development guidelines for AI assistants
- **[Bugs.md](Bugs.md)** - Known issues and bug tracking
- **[Updates.md](Updates.md)** - Planned features and roadmap

---

## 🗺️ Roadmap

### Phase 1: Core Expansion
- [ ] Hexagonal grid support (the package name promises it!)
- [ ] Grid query utilities (neighbors, ranges, pathfinding)
- [ ] Enhanced coordinate system utilities
- [ ] Unit test coverage

### Phase 2: Data & Visualization
- [ ] Multi-layer grid data system
- [ ] Enhanced visualization tools (heatmaps, vector fields)
- [ ] Grid event system for reactive programming
- [ ] Improved save/load with compression & versioning

### Phase 3: Advanced Features
- [ ] Procedural generation pipelines (noise, cellular automata)
- [ ] Grid transformation utilities (rotation, mirroring)
- [ ] Async/await API wrappers
- [ ] Performance optimizations (sparse grids, streaming)

See [Updates.md](Updates.md) for the complete feature wishlist.

---

## 🤝 Contributing

This is a **proprietary internal tool** for the To The Moon project. External contributions are not accepted.

For internal development:
- Maintain Burst-compatible code for performance-critical paths
- Use QR axial coordinates (no UV coordinates)
- Follow existing job runner patterns
- Add XML documentation for public APIs
- Keep backward compatibility

---

## 📄 License

**Proprietary - All Rights Reserved**

This package is proprietary software developed for internal use by the To The Moon project team. It is not licensed for external use, modification, or distribution.

© 2025 To The Moon Project. All rights reserved.

---

## 👥 Authors

- **Ava** - Original Author
- **Contributors** - See GitHub contributors

---

## 🔗 Related Projects

- **Moon Plots** - Plot management system
- **Cartographer** - World generation and mapping tools

---

## 📝 Notes

- Grid dimensions are automatically rounded to even numbers
- All job runners must dispose their `NativeArray` allocations
- `GridRenderer` uses `UnityEditor` and only works in editor mode
- Atomic operations require assembly definition with `allowUnsafeCode: true`

---

## 💡 Examples

### Custom Grid Job

```csharp
using HexWorldUtils.Job.Grid;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

[BurstCompile]
public struct MyCustomGridJob : IJobFor
{
    [ReadOnly] public int Width;
    [WriteOnly] public NativeArray<float> Output;
    
    public void Execute(int index)
    {
        SharedJobHelper.IndexToGrid(index, Width, out var x, out var y);
        Output[index] = x * y; // Your logic here
    }
}
```

### Grid Visualization

```csharp
using HexWorldUtils.GridSystem;
using UnityEngine;

[RequireComponent(typeof(GridHandler))]
public class GridVisualizer : MonoBehaviour
{
    private GridRenderer renderer;
    
    private void Awake()
    {
        renderer = gameObject.AddComponent<GridRenderer>();
        // Configure visualization in inspector
    }
}
```

---

**Made with ❤️ for the To The Moon project**
