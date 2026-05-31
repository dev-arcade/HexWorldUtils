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
        public static int2 RotateRight(int2 p, int rotationStep)
        {
            const int cx = 500;
            const int cy = 500;

            int k = (-rotationStep) % 6;
            if (k < 0)
                k += 6;

            int dx = p.x - cx;
            int dy = p.y - cy;

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

            float rx = dx * cos - dy * sin;
            float ry = dx * sin + dy * cos;

            int x2 = (int)MathF.Round(rx) + cx;
            int y2 = (int)MathF.Round(ry) + cy;

            return new int2(x2, y2);
        }
    }
}
