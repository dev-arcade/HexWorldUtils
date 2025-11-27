using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MoonPlotsCartographerShared.Job
{
    public static class SharedJobHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int AtomicIncrement(NativeArray<int> counter) =>
            Interlocked.Increment(ref ((int*)counter.GetUnsafePtr())[0]) - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int AtomicIncrementArray(NativeArray<int> array, int index) =>
            Interlocked.Increment(ref ((int*)array.GetUnsafePtr())[index]) - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int AtomicDecrementArray(NativeArray<int> array, int index) =>
            Interlocked.Decrement(ref ((int*)array.GetUnsafePtr())[index]) - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int AtomicExchangeArray(NativeArray<int> array, int index, int newValue) =>
            Interlocked.Exchange(ref ((int*)array.GetUnsafePtr())[index], newValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBatchCount(int length)
        {
            int workerCount = JobsUtility.JobWorkerCount;
            int targetBatches = workerCount * 8;
            int batchCount = math.max(length / targetBatches, 1);
            return math.clamp(batchCount, 32, 512);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IndexToGrid(int index, int width, out int x, out int y)
        {
            x = index % width;
            y = index / width;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GridToIndex(int x, int y, int width) => y * width + x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetRandomHash(uint x)
        {
            x ^= x >> 16;
            x *= 0x7feb352d;
            x ^= x >> 15;
            x *= 0x846ca68b;
            x ^= x >> 16;
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetOverrideHeatAccept() => 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHeatOverriddenAccept(float heat) => heat >= 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetOverrideHeatIgnore() => 4;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHeatOverriddenIgnore(float heat) => heat >= 4;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDisableClusterPoint() => -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDisabledClusterPoint(int value) => value == -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ScalePoint(float2 srcPoint, int srcWidth, int srcHeight, int dstWidth, int dstHeight)
        {
            return new float2(
                srcPoint.x * (dstWidth / (float)srcWidth),
                srcPoint.y * (dstHeight / (float)srcHeight));
        }
    }
}
