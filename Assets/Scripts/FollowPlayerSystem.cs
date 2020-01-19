using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
#if UNITY_DOTSPLAYER
using Unity.Tiny.Rendering;
using Unity.Tiny.Input;

#endif
[AlwaysSynchronizeSystem]
public class FollowPlayerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var followSpeed = 3;
        var offset = new float3(0, 0, -25);
        var deltaTime = Time.DeltaTime;
        Entities.WithoutBurst().WithAll<Player>().ForEach((ref Translation translation) =>
        {
#if UNITY_DOTSPLAYER
            var cameraEntity = GetSingletonEntity<Camera>();
            var cameraPos = EntityManager.GetComponentData<Translation>(cameraEntity).Value;
            EntityManager.SetComponentData(cameraEntity, new Translation { Value = math.lerp(cameraPos, translation.Value + offset, deltaTime * followSpeed) });
#else
            var mainCamPos = UnityEngine.Camera.main.transform;
            mainCamPos.position = math.lerp(mainCamPos.position, translation.Value + offset, deltaTime * followSpeed);
#endif
        }).Run();
        return default;
    }
}