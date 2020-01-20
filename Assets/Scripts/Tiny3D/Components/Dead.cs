using Unity.Entities;

[GenerateAuthoringComponent]
public struct Dead : IComponentData
{
    public bool IsDead;
}
