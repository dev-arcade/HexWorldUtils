using System;
using Unity.Mathematics;
using UnityEngine;

namespace HexWorldUtils.GridSystem
{
    [Serializable]
    public class GridSettings
    {
        [SerializeField] private float3 originPosition;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSizeX;
        [SerializeField] private float cellSizeY;

        public float3 OriginPosition => originPosition;
        public int Width => width;
        public int Height => height;
        public float CellSizeX => cellSizeX;
        public float CellSizeY => cellSizeY;

        public GridSettings(float3 originPosition, int width, int height, float cellSizeX, float cellSizeY)
        {
            if (!GridValidator.ValidateDimensions(width, height) ||
                !GridValidator.ValidateCellSize(cellSizeX, cellSizeY))
                return;

            this.originPosition = originPosition;
            this.width = width % 2 > 0 ? width + 1 : width;
            this.height = height % 2 > 0 ? height + 1 : height;
            this.cellSizeX = cellSizeX;
            this.cellSizeY = cellSizeY;
        }
    }
}
