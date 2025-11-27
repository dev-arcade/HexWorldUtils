using HexWorldUtils.Job.General;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace HexWorldUtils.Job.Grid
{
    public class RandomGridPointGeneratorRunner
    {
        public NativeArray<float2> Complete(int width, int height, int minDistance, float density, int targetWidth,
            int targetHeight)
        {
            var hashWidth = (int)math.ceil((float)width / minDistance);
            var hashHeight = (int)math.ceil((float)height / minDistance);
            var cellCount = hashWidth * hashHeight;

            var outputMask = new NativeArray<byte>(cellCount, Allocator.TempJob);
            var hashOccupied = new NativeArray<int>(cellCount, Allocator.TempJob);
            var hashPositions = new NativeArray<float2>(cellCount, Allocator.TempJob);
            var job = new SpatialHashRandomPointJob
            {
                Width = width,
                Height = height,
                TargetWidth = targetWidth,
                TargetHeight = targetHeight,
                MinDistance = minDistance,
                MinDistanceSq = minDistance * minDistance,
                HashWidth = hashWidth,
                HashHeight = hashHeight,
                Density = density,
                RandomSeed = (uint)System.Environment.TickCount,
                OutputMask = outputMask,
                HashOccupied = hashOccupied,
                HashPositions = hashPositions,
            };

            var batchCount = SharedJobHelper.GetBatchCount(cellCount);
            var handle = job.ScheduleParallel(cellCount, batchCount, default);
            handle.Complete();

            var results = new NativeArray<float2>(cellCount, Allocator.TempJob);
            var fillerJob = new ArrayFillerJob<float2>()
            {
                Input = hashPositions,
                Count = cellCount,
                Output = results,
            };

            batchCount = SharedJobHelper.GetBatchCount(cellCount);
            var fillerHandle = fillerJob.ScheduleParallel(cellCount, batchCount, default);
            fillerHandle.Complete();

            outputMask.Dispose();
            hashOccupied.Dispose();
            hashPositions.Dispose();
            return results;
        }
    }

    [BurstCompile]
    public struct SpatialHashRandomPointJob : IJobFor
    {
        [ReadOnly] public int Width;
        [ReadOnly] public int Height;
        [ReadOnly] public int TargetWidth;
        [ReadOnly] public int TargetHeight;
        [ReadOnly] public int MinDistance;
        [ReadOnly] public float MinDistanceSq;
        [ReadOnly] public int HashWidth;
        [ReadOnly] public int HashHeight;
        [ReadOnly] public float Density;
        [ReadOnly] public uint RandomSeed;

        [NativeDisableParallelForRestriction] public NativeArray<byte> OutputMask;
        [NativeDisableParallelForRestriction] public NativeArray<int> HashOccupied;
        [NativeDisableParallelForRestriction] public NativeArray<float2> HashPositions;

        public void Execute(int index)
        {
            var rng = new Random(SharedJobHelper.GetRandomHash(RandomSeed + (uint)index));
            var fx = rng.NextFloat(0f, Width);
            var fy = rng.NextFloat(0f, Height);

            var cellX = (int)math.floor(fx / MinDistance);
            var cellY = (int)math.floor(fy / MinDistance);
            if (cellX < 0 || cellX >= HashWidth || cellY < 0 || cellY >= HashHeight)
                return;

            var selfHash = cellY * HashWidth + cellX;
            if (HashOccupied[selfHash] != 0)
                return;

            var minHx = math.max(0, cellX - 1);
            var maxHx = math.min(HashWidth - 1, cellX + 1);
            var minHy = math.max(0, cellY - 1);
            var maxHy = math.min(HashHeight - 1, cellY + 1);

            var pos = new float2(fx, fy);
            for (var cy = minHy; cy <= maxHy; cy++)
            {
                var rowOffset = cy * HashWidth;
                for (var cx = minHx; cx <= maxHx; cx++)
                {
                    var hIndex = rowOffset + cx;
                    if (HashOccupied[hIndex] != 0)
                    {
                        var occupyingPos = HashPositions[hIndex];
                        var distSq = math.distancesq(pos, occupyingPos);
                        if (distSq < MinDistanceSq)
                            return;
                    }
                }
            }

            if (SharedJobHelper.AtomicExchangeArray(HashOccupied, selfHash, 1) != 0)
                return;

            if (rng.NextFloat() > Density)
                return;

            HashPositions[selfHash] = SharedJobHelper.ScalePoint(pos, Width, Height, TargetWidth, TargetHeight);
            OutputMask[index] = 1;
        }
    }
}
