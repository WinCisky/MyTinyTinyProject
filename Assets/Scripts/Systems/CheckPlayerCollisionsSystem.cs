using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CheckPlayerCollisionsSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        //get player position
        var playerPosition = new float2(0, 0);
        Entities.WithAll<Player>().ForEach((Entity ent, ref Translation pos) =>
        {
            playerPosition = new float2(pos.Value.x, pos.Value.y);
            Entities.WithAll<ObstacleTag>().ForEach((ref Translation translation) =>
            {
                if (translation.Value.z >= -10)
                {
                    if (math.abs(playerPosition.y-translation.Value.y) < 0.95f && math.abs(playerPosition.x - translation.Value.x) < 0.95f)
                    {
                        //collision occurred -> GameOver
                        EntityManager.SetComponentData(ent, new Dead { IsDead = true });
                    }
                }
            });
        });
    }
}