using HexWorldUtils.GridSystem;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace HexWorldUtils.Job.Grid
{
    public class GridCalculatorRunner
    {
        public float3[,] Complete(int width, int height, float2 cellSize, float3 origin, JobHandle dependency = default)
        {
            var positions = new NativeArray<float3>(width * height, Allocator.TempJob);
            var handle = Schedule(positions, width, height, cellSize, origin, dependency);
            handle.Complete();
            var array = Get2DArray(positions, width, height);
            positions.Dispose();
            return array;
        }

        public JobHandle Schedule(NativeArray<float3> positions, int width, int height, float2 cellSize, float3 origin,
            JobHandle dependency = default)
        {
            var job = new GridCalculatorJob()
            {
                Positions = positions,
                Width = width,
                Height = height,
                CellSize = cellSize,
                Origin = origin
            };

            var length = width * height;
            var batchCount = SharedJobHelper.GetBatchCount(length);
            return job.ScheduleParallel(length, batchCount, dependency);
        }

        private float3[,] Get2DArray(NativeArray<float3> flatArray, int width, int height)
        {
            var data = new float3[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var index = y * width + x;
                    data[x, y] = flatArray[index];
                }
            }

            return data;
        }
    }

    [BurstCompile]
    public struct GridCalculatorJob : IJobFor
    {
        [ReadOnly] public int Width;
        [ReadOnly] public int Height;
        [ReadOnly] public float2 CellSize;
        [ReadOnly] public float3 Origin;

        [WriteOnly] public NativeArray<float3> Positions;

        public void Execute(int index)
        {
            SharedJobHelper.IndexToGrid(index, Width, out var x, out var y);
            Positions[index] = GridMath.GridToWorld2D(Origin, CellSize, x, y);
        }
    }
}
