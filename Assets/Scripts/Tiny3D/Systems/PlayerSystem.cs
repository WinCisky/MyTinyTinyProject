using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class PlayerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Translation translation, ref PlayerInput playerInput, in Player player) =>
        {
            if (playerInput.actionA)
            {
                playerInput.actionA = false;
                //perform action a
                translation.Value += new float3(player.MovementSpeed * deltaTime, 0, 0);
            }

            if (playerInput.actionB)
            {
                playerInput.actionB = false;
                //perform action b
                translation.Value -= new float3(player.MovementSpeed * deltaTime, 0, 0);
            }
        }).Run();
        return default;
    }
}