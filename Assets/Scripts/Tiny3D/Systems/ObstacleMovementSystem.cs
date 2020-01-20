using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class ObstacleMovementSystem : JobComponentSystem
{
    //[BurstCompile]
    struct ObstacleMovementSystemJob : IJobForEach<Translation, ObstacleTag>
    {
        public bool gameStarted;
        public double elapsedTime;
        public double deltaTime;
        public float2 playerPosition;
        public float movingDirection;
        public bool isRotating;

        public void Execute(ref Translation translation, ref ObstacleTag ot)
        {
            if (gameStarted)
            {
                //slow down if necessary
                ot.StartTime += /*(movingDirection > 0 ? 1 : -1)*/ 1 * (deltaTime - (deltaTime * abs(movingDirection)));
                //move vertically
                ot.StartTime += (movingDirection < 0 ? 1 : 0) * abs(movingDirection) * 2 * deltaTime;
                translation.Value = 
                    new float3(
                        ot.startPos.x,
                        ((-(float)(elapsedTime - ot.StartTime) % 40) * 
                        ot.MovementSpeed + ot.startPos.y), 
                        ot.startPos.z
                    );

                if (!isRotating)
                {
                    //top-bottom
                    if (abs(translation.Value.y) >= 30 && !ot.Swapped)
                    {
                        ot.Swapped = true;
                        if (movingDirection > 0)
                            ot.StartTime += 12;
                        else
                            ot.StartTime -= 12;
                        ot.startPos.z = (noise.cnoise(new float2(ot.startPos.x, ot.startPos.y + (float)(elapsedTime - ot.StartTime)) * 0.21F) + 0.2) > 0 ? -100 : 0;
                    }
                    else if (abs(translation.Value.y) < 10 && ot.Swapped)
                        ot.Swapped = false;

                    //right-left
                    if (abs(translation.Value.x - playerPosition.x) > 30)
                    {
                        if (playerPosition.x > translation.Value.x)
                            ot.startPos.x += 60;
                        else
                            ot.startPos.x -= 60;
                    }
                }
            }
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var gameStart = false;
        Entities.ForEach((in GameStatus gs) =>
        {
            gameStart = gs.started;
        }).Run();

        //get player position
        var playerPos = new float2(0, 0);
        Entities.WithAll<Player>().ForEach((ref Translation pos) =>
        {
            playerPos = new float2(pos.Value.x, pos.Value.y);
        }).Run();

        //get movement status
        var movingDir = 0f;
        Entities.ForEach((in CubeMovementStatus cms) =>
        {
            movingDir = cms.movingSpeed;
        }).Run();

        var rotating = false;
        Entities.ForEach((in Confusion conf) =>
        {
            rotating = conf.rotate;
        }).Run();

        //move cubes according to player pos
        var job = new ObstacleMovementSystemJob
        {
            gameStarted = gameStart,
            elapsedTime = Time.ElapsedTime,
            playerPosition = playerPos,
            movingDirection = movingDir,
            deltaTime = Time.DeltaTime,
            isRotating = rotating
        };

        var jobHandle = job.Schedule(this, inputDependencies);

        // Now that the job is set up, schedule it to be run. 
        return jobHandle;
    }
}