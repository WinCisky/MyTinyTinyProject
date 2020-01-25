using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

public class GameStatusSystem : ComponentSystem
{
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
            else
            {
                gs.startTime += Time.DeltaTime;
            }
        });
        //update ui
        Entities.ForEach((ref Digit d) =>
        {
            if(!d.mainMenu)
                d.isActive = started;

            if (!started)
                return;

            if (d.digit == 1)
            {
                d.shownValue = ((int)(score / 100)).ToString()[0];
            }
            else if (d.digit == 2)
            {
                d.shownValue = ((int)(score / 10)).ToString()[0];
            }
            else if (d.digit == 3)
            {
                d.shownValue = ((int)(score % 10)).ToString()[0];
            }
            else
            {
                d.isActive = false;
            }

            //center digits
            if (score <= 9)
            {
                if (d.digit == 3)
                {
                    d.offset = new Unity.Mathematics.float3(0, 10, 20);
                    d.isActive = true;
                }
                else
                    d.isActive = false;
            }
            else if (score <= 99)
            {
                if (d.digit == 3)
                {
                    d.offset = new Unity.Mathematics.float3(0.4f, 10, 20);
                    d.isActive = true;
                }
                else if (d.digit == 2)
                {
                    d.offset = new Unity.Mathematics.float3(-0.4f, 10, 20);
                    d.isActive = true;
                }
                else
                    d.isActive = false;
            }
            else
            {
                d.isActive = true;
                if (d.digit == 3)
                    d.offset = new Unity.Mathematics.float3(0.8f, 10, 20);
                else if (d.digit == 2)
                    d.offset = new Unity.Mathematics.float3(0f, 10, 20);
                else
                    d.offset = new Unity.Mathematics.float3(-0.8f, 10, 20);
            }
        });
    }
}