using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class DigitCubeSystem : JobComponentSystem
{
    [BurstCompile]
    struct DigitCubeSystemJob : IJobForEach<Translation, DigitCube>
    {
        public void Execute(ref Translation translation, [ReadOnly] ref DigitCube dc)
        {
            if (dc.isActive)
                translation.Value.z = 0;
            else
                translation.Value.z = -100;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new DigitCubeSystemJob();
        return job.Schedule(this, inputDependencies);
    }
}