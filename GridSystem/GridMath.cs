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
        public static int2 WorldToGrid3D(float3 worldPosition, float3 originPosition, float cellSizeX, float cellSizeY)
        {
            var offset = worldPosition - originPosition;
            return new int2((int)math.floor(offset.x / cellSizeX), (int)math.floor(offset.z / cellSizeY));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 GridToWorld2D(float3 origin, float2 cellSize, int x, int y)
        {
            var worldX = origin.x + (x * cellSize.x);
            var worldY = origin.y + (y * cellSize.y);
            return new float3(worldX, worldY, origin.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 GridToWorld3D(float3 origin, float2 cellSize, int x, int y)
        {
            var worldX = origin.x + (x * cellSize.x);
            var worldZ = origin.z + (y * cellSize.y);
            return new float3(worldX, origin.y, worldZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 RotateRight(int2 p, int rotation)
        {
            const int cx = 500;
            const int cy = 500;

            var k = (-rotation) % 6;
            if (k < 0)
                k += 6;

            var dx = p.x - cx;
            var dy = p.y - cy;

            float cos, sin;
            switch (k)
            {
                case 0:
                    cos = 1f;
                    sin = 0f;
                    break;
                case 1:
                    cos = 0.5f;
                    sin = MathF.Sqrt(3f) * 0.5f;
                    break;
                case 2:
                    cos = -0.5f;
                    sin = MathF.Sqrt(3f) * 0.5f;
                    break;
                case 3:
                    cos = -1f;
                    sin = 0f;
                    break;
                case 4:
                    cos = -0.5f;
                    sin = -MathF.Sqrt(3f) * 0.5f;
                    break;
                case 5:
                    cos = 0.5f;
                    sin = -MathF.Sqrt(3f) * 0.5f;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            var rx = dx * cos - dy * sin;
            var ry = dx * sin + dy * cos;

            var x2 = (int)math.round(rx) + cx;
            var y2 = (int)math.round(ry) + cy;

            return new int2(x2, y2);
        }
    }
}
