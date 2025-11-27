using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace HexWorldUtils.GridSystem
{
    public static class GridMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 WorldToGrid2D(float3 worldPosition, float3 originPosition, float cellSizeX, float cellSizeY)
        {
            var offset = worldPosition - originPosition;
            return new int2((int)math.floor(offset.x / cellSizeX), (int)math.floor(offset.y / cellSizeY));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 GridToWorld2D(float3 origin, float2 cellSize, int x, int y)
        {
            var worldX = origin.x + (x * cellSize.x);
            var worldY = origin.y + (y * cellSize.y);
            return new float3(worldX, worldY, 0);
        }
    }
}
