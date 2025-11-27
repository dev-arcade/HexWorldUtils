using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace HexWorldUtils.Job.General
{
    [BurstCompile]
    public struct ArrayFillerJob<T> : IJobFor where T : struct
    {
        [ReadOnly] public NativeArray<T> Input;
        [ReadOnly] public int Count;

        [WriteOnly] public NativeArray<T> Output;

        public void Execute(int index)
        {
            if (index < Count)
                Output[index] = Input[index];
        }
    }
}
