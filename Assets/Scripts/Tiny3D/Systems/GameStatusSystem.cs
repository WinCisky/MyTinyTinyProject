using Unity.Entities;

public class GameStatusSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref GameStatus gs) =>
        {
            gs.score = ((int)(Time.ElapsedTime - gs.startTime) % 10);
        });
    }
}