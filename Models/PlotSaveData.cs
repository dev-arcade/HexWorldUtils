using System;
using Newtonsoft.Json;
using Unity.Mathematics;

namespace HexWorldUtils.Models
{
    public class PlotSaveData
    {
        [JsonProperty] public Guid ID;
        [JsonProperty] public string TerrainID;
        [JsonProperty] public int2 GridSize;
        [JsonProperty] public int2 PivotPoint;
        [JsonProperty] public string[] Items; // "{Category}{Type}{Subtype}:{Pos}:{Scale}"

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
