using Unity.Entities;

[GenerateAuthoringComponent]
public struct Player : IComponentData
{
    public float MovementSpeed;
}
