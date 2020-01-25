using Unity.Entities;

[GenerateAuthoringComponent]
public struct Spawner : IComponentData
{
    public int CountX, CountY;
    public Entity Prefab;
}
