using Unity.Entities;

[GenerateAuthoringComponent]
public struct Confusion : IComponentData
{
    public bool running;

    public float currentElapsedTime;
    public float confusionCooldown;

    public bool wasGoingDown;
    public bool slowDownTime;
    public bool rotate;
    public bool fakeRotate;
    public bool speedUpTime;

    public float targetRotation;
}
