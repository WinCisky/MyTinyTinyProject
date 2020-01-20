using Unity.Entities;

[GenerateAuthoringComponent]
public struct PlayerInput : IComponentData
{
    public bool actionA, actionB;
}
