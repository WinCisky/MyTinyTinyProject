using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class DeathSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        //var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        Entities.WithAll<Player>().ForEach((ref Dead dead) =>
        {
            if (dead.IsDead)
            {
                dead.IsDead = false;
                //stop confusion system
                Entities.ForEach((ref Confusion confusion) =>
                {
                    confusion.running = false;
                    confusion.currentElapsedTime = 0;
                    confusion.wasGoingDown = true;
                    confusion.rotate = false;
                    confusion.speedUpTime = false;
                    confusion.slowDownTime = false;
                });

                Entities.ForEach((ref GameStatus gs) =>
                {
                    gs.gameStarted = false;
                    gs.gameOver = true;
                });

                //reposition score recap (main menu)
                var cameraPos = new float3(0, 0, 0);
                Entities.ForEach((ref PlayerPosition cp) =>
                {
                    cameraPos = cp.pos;
                });
                Entities.ForEach((ref Translation translation, ref Digit d) =>
                {
                    if (d.mainMenu)
                        translation.Value = cameraPos + d.offset;
                });
                //destroy Obstacles
                var obj = GetEntityQuery(new ComponentType[] { typeof(ObstacleTag) });
                EntityManager.DestroyEntity(obj);
                
                //show ui -> (restart, exit)

                //show score

            }
        });

        //ecb.Playback(EntityManager);
        //ecb.Dispose();
    }
}