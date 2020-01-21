using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

public class GameStatusSystem : ComponentSystem
{
    protected override void OnStartRunning()
    {
        Entities.ForEach((ref GameStatus gs) =>
        {
            gs.gameStarted = true;
            gs.startTime = Time.ElapsedTime;
        });
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        var score = 0;
        var started = false;
        Entities.ForEach((ref GameStatus gs) =>
        {
            started = gs.gameStarted;
            if (started)
            {
                score = (int)(Time.ElapsedTime - gs.startTime);
                gs.score = score;
            }
        });
        //update ui
        Entities.ForEach((ref Digit d) =>
        {
            d.isActive = started;
            if (!started)
                return;

            if (d.digit == 1)
            {
                d.shownValue = (int)(score / 100);
            }
            else if (d.digit == 2)
            {
                d.shownValue = (int)(score / 10);
            }
            else if (d.digit == 3)
            {
                d.shownValue = (int)(score % 10);
            }
        });
    }
}