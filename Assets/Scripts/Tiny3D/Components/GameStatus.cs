using Unity.Entities;

[GenerateAuthoringComponent]
public struct GameStatus : IComponentData
{
    public bool started;
    public int score;

    public double startTime;

    public Entity scoreDigit1;
    public Entity scoreDigit2;
    public Entity scoreDigit3;
}
