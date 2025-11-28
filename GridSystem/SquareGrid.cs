using System;
using HexWorldUtils.Job.Grid;
using Unity.Mathematics;

namespace HexWorldUtils.GridSystem
{
    public class SquareGrid
    {
        private readonly float3 _originPosition;
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSizeX;
        private readonly float _cellSizeY;
        private readonly float3[,] _gridData;

        public float3[,] GridData => _gridData;
        public float3 Origin => _originPosition;
        public int Width => _width;
        public int Height => _height;
        public float CellSizeX => _cellSizeX;
        public float CellSizeY => _cellSizeY;

        public SquareGrid(float3 originPosition, int width, int height, float cellSizeX, float cellSizeY)
        {
            if (!GridValidator.ValidateDimensions(width, height) ||
                !GridValidator.ValidateCellSize(cellSizeX, cellSizeY))
                return;

            _originPosition = originPosition;
            _width = width;
            _height = height;
            _cellSizeX = cellSizeX;
            _cellSizeY = cellSizeY;
            var runner = new GridCalculatorRunner();
            _gridData = runner.Complete(width, height, new float2(cellSizeX, cellSizeY), _originPosition);
        }

        public int2 WorldToGrid2D(float3 worldPosition) =>
            GridMath.WorldToGrid2D(worldPosition, _originPosition, _cellSizeX, _cellSizeY);

        public float3 GridToWorld2D(int2 gridAddress) =>
            GridMath.GridToWorld2D(_originPosition, new float2(_cellSizeX, _cellSizeY), gridAddress.x, gridAddress.y);
        
        public int2 WorldToGrid3D(float3 worldPosition) =>
            GridMath.WorldToGrid3D(worldPosition, _originPosition, _cellSizeX, _cellSizeY);
        
        public int2 WorldToGrid3D(float3 worldPosition, float3 origin) =>
            GridMath.WorldToGrid3D(worldPosition, origin, _cellSizeX, _cellSizeY);

        public float3 GridToWorld3D(int2 gridAddress) =>
            GridMath.GridToWorld3D(_originPosition, new float2(_cellSizeX, _cellSizeY), gridAddress.x, gridAddress.y);
        
        public float3 GridToWorld3D(int2 gridAddress, float3 origin) =>
            GridMath.GridToWorld3D(origin, new float2(_cellSizeX, _cellSizeY), gridAddress.x, gridAddress.y);

        public float3 WorldToGridWorld2D(float3 worldPosition, out int2 gridAddress)
        {
            gridAddress = WorldToGrid2D(worldPosition);
            return GridToWorld2D(gridAddress);
        }
        
        public float3 WorldToGridWorld3D(float3 worldPosition, out int2 gridAddress)
        {
            gridAddress = WorldToGrid3D(worldPosition);
            return GridToWorld3D(gridAddress);
        }

        public bool IsInsideGrid(int2 gridAddress) => gridAddress.x >= 0 && gridAddress.x < Width &&
                                                      gridAddress.y >= 0 && gridAddress.y < Height;
    }
}
