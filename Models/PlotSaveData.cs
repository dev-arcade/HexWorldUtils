using System;
using Unity.Mathematics;

namespace HexWorldUtils.Models
{
    public class PlotSaveData
    {
        public Guid ID;
        public string TerrainID;
        public int2 GridSize;
        public int2 PivotPoint;
        public string[] Items; // "{Category}{Type}{Subtype}:{Pos}:{Scale}"

        public PlotSaveData() { }

        public PlotSaveData(Guid id, string terrainID, int2 gridSize, int2 pivotPoint, string[] items)
        {
            ID = id;
            TerrainID = terrainID;
            GridSize = gridSize;
            PivotPoint = pivotPoint;
            Items = items;
        }
    }
}
