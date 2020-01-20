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
        Entities.WithAll<Player>().ForEach((ref Translation translation) =>
        {

            var playerPos = translation.Value;

#if UNITY_DOTSPLAYER
            var cameraEntity = GetSingletonEntity<Camera>();
            var cameraPos = EntityManager.GetComponentData<Translation>(cameraEntity).Value;
            EntityManager.SetComponentData(cameraEntity, new Translation { Value = math.lerp(cameraPos, playerPos + offset, deltaTime * followSpeed) });
#else
            var cameraPos = UnityEngine.Camera.main.transform.position;
            cameraPos = math.lerp(cameraPos, playerPos + offset, deltaTime * followSpeed);
#endif

            var gameStarted = false;
            Entities.ForEach((ref GameStatus gameStatus) =>
            {
                gameStarted = gameStatus.started;
            });

            if (gameStarted)
            {
                //update score pos (ui)
                Entities.WithNone<Disabled>().ForEach((ref Translation scoreDigitPos, ref ScoreDigit scoreDigit) =>
                {
                    scoreDigitPos.Value = (float3)cameraPos + scoreDigit.offset;
                });
            }
        });
    }
}