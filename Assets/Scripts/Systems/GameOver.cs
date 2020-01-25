using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

public class GameOver : ComponentSystem
{
    protected override void OnUpdate()
    {
        var gameOver = false;
        var score = 0;
        Entities.ForEach((ref GameStatus gs) =>
        {
            gameOver = gs.gameOver;
            score = gs.score;
        });
        if (gameOver)
        {
            Entities.ForEach((ref Digit d) =>
            {
                d.isActive = true;
                switch (d.row)
                {
                    case 1:
                        var s1 = "   your score was:   ";
                        d.shownValue = s1[d.charIndex];
                        break;
                    case 2:
                        var s2 = "   ";
                        if (score <= 9)
                            s2 += ' ';
                        if (score <= 99)
                            s2 += ' ';
                        s2 += score.ToString();
                        s2 += "    ";
                        d.shownValue = s2[d.charIndex];
                        break;
                    case 3:
                        var s3 = "congratulations!";
                        d.shownValue = s3[d.charIndex];
                        break;
                }
            });
        }
    }
}