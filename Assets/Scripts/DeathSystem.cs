using Unity.Entities;
using Unity.Jobs;
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
                });

                //destroy Obstacles
                var obj = GetEntityQuery(new ComponentType[] { typeof(ObstacleTag) });
                EntityManager.DestroyEntity(obj);

                //TODO
                //block input

                //show ui -> (restart, exit)

                //show score


                //temporary -> restart
                Entities.ForEach((ref Confusion confusion) =>
                {
                    //reset status
                    confusion.currentElapsedTime = 0;
                    confusion.wasGoingDown = true;
                    confusion.running = true;
                    confusion.rotate = false;
                    confusion.speedUpTime = false;
                    confusion.slowDownTime = false;
                });
                Entities.ForEach((ref CubeMovementStatus cms) =>
                {
                    cms.movingSpeed = 1;
                });
                Entities.ForEach((ref Prefabs prefabs) =>
                {
                    EntityManager.Instantiate(prefabs.Spawner);
                });
                Entities.ForEach((ref GameStatus gs) =>
                {
                    gs.gameStarted = true;
                    gs.startTime = Time.ElapsedTime;
                });
            }
        });

        //ecb.Playback(EntityManager);
        //ecb.Dispose();
    }
}