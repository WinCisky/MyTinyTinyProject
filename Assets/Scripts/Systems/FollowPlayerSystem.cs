using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Tiny.Rendering;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class FollowPlayerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var followSpeed = 3;
        var offset = new float3(0, 0, -25);
        var deltaTime = Time.DeltaTime;
        var cameraPos = new float3(0, 0, 0); 
        var playerPos = new float3(0, 0, 0);

        Entities.WithAll<Player>().ForEach((ref Translation translation) =>
        {
            var cameraEntity = GetSingletonEntity<Camera>();
            cameraPos = EntityManager.GetComponentData<Translation>(cameraEntity).Value;
            EntityManager.SetComponentData(cameraEntity, new Translation { Value = math.lerp(cameraPos, translation.Value + offset, deltaTime * followSpeed) });
            playerPos = translation.Value;
        });

        //NOT WORKING! -> can't change the background color at runtime :(
        //Entities.ForEach((ref Camera cam) =>
        //{
        //    cam.backgroundColor.r = 0;
        //    cam.backgroundColor.g = 0;
        //    cam.backgroundColor.b = 0;
        //    cam.backgroundColor.Value = new float4(0, 0, 0, 0);
        //});

        //let the ui follow the player
        Entities.ForEach((ref Translation translation, ref Digit d) =>
        {
            if (!d.mainMenu)
                translation.Value = cameraPos + d.offset;
        });

        //update camera pos
        Entities.ForEach((ref PlayerPosition pp) =>
        {
            pp.pos = playerPos;
        });
    }
}