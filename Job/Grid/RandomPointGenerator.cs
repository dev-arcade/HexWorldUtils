using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace HexWorldUtils.Job.Grid
{
    public class RandomPointGeneratorRunner
    {
        public NativeArray<byte> Complete(NativeArray<int> includedPoints, int width, int height, int minDistance,
            float density)
        {
            var hashWidth = (width + minDistance - 1) / minDistance;
            var totalLength = width * height;
            var outMask = new NativeArray<byte>(totalLength, Allocator.TempJob);
            var hashOccupied = new NativeArray<int>(hashWidth * hashWidth, Allocator.TempJob);
            var hashPositions = new NativeArray<float2>(hashWidth * hashWidth, Allocator.TempJob);

            var shuffledPoints = new NativeArray<int>(includedPoints.Length, Allocator.TempJob);
            shuffledPoints.CopyFrom(includedPoints);

            var seed = (uint)System.DateTime.Now.Ticks;
            var shuffleJob = new ShuffleIndicesJob
            {
                Indices = shuffledPoints,
                RandomSeed = seed
            };

            shuffleJob.Schedule().Complete();

            var processJob = new SpatialHashDistanceJob
            {
                IncludedPoints = shuffledPoints,
                OutputMask = outMask,
                HashOccupied = hashOccupied,
                HashPositions = hashPositions,
                Width = width,
                MinDistance = minDistance,
                MinDistanceSq = minDistance * minDistance,
                HashWidth = hashWidth,
                HashHeight = hashWidth,
                Density = density,
                RandomSeed = seed,
            };

            var batchCount = SharedJobHelper.GetBatchCount(shuffledPoints.Length);
            var handle = processJob.ScheduleParallel(shuffledPoints.Length, batchCount, default);
            handle.Complete();

            hashOccupied.Dispose();
            hashPositions.Dispose();
            shuffledPoints.Dispose();

            return outMask;
        }
    }

    [BurstCompile]
    public struct ShuffleIndicesJob : IJob
    {
        [ReadOnly] public uint RandomSeed;
        public NativeArray<int> Indices;

        public void Execute()
        {
            var rng = new Random(SharedJobHelper.GetRandomHash(RandomSeed));
            int n = Indices.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.NextInt(0, i + 1);
                (Indices[i], Indices[j]) = (Indices[j], Indices[i]);
            }
        }
    }

    [BurstCompile]
    public struct SpatialHashDistanceJob : IJobFor
    {
        [ReadOnly] public NativeArray<int> IncludedPoints;
        [ReadOnly] public int Width;
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
            var point = IncludedPoints[index];
            if (point < 0)
                return;

            SharedJobHelper.IndexToGrid(point, Width, out var x, out var y);
            var rng = new Random(SharedJobHelper.GetRandomHash(RandomSeed + (uint)point));

            const float jitterAmount = 0.65f;
            var jx = (rng.NextFloat() - 0.5f) * 2f * jitterAmount * MinDistance;
            var jy = (rng.NextFloat() - 0.5f) * 2f * jitterAmount * MinDistance;

            var fx = x + jx;
            var fy = y + jy;

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

            if (SharedJobHelper.AtomicExchangeArray(HashOccupied, selfHash, point + 1) != 0)
                return;

            if (rng.NextFloat() > Density)
                return;

            HashPositions[selfHash] = pos;
            OutputMask[point] = 1;
        }
    }
}
