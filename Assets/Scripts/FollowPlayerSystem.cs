using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
#if UNITY_DOTSPLAYER
using Unity.Tiny.Rendering;
using Unity.Tiny.Input;

#endif
[AlwaysSynchronizeSystem]
public class FollowPlayerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var followSpeed = 3;
        var offset = new float3(0, 0, -25);
        var deltaTime = Time.DeltaTime;
        var cameraPos = new float3(0, 0, 0); 
        Entities.WithAll<Player>().ForEach((ref Translation translation) =>
        {
#if UNITY_DOTSPLAYER
            var cameraEntity = GetSingletonEntity<Camera>();
            cameraPos = EntityManager.GetComponentData<Translation>(cameraEntity).Value;
            EntityManager.SetComponentData(cameraEntity, new Translation { Value = math.lerp(cameraPos, translation.Value + offset, deltaTime * followSpeed) });
#else
            cameraPos = UnityEngine.Camera.main.transform.position;
            cameraPos = math.lerp(cameraPos, translation.Value + offset, deltaTime * followSpeed);
#endif
        });

        //let the ui follow the player
        Entities.ForEach((ref Translation translation, ref Digit d) =>
        {
            if (!d.mainMenu)
                translation.Value = cameraPos + d.offset;
        });
    }
}