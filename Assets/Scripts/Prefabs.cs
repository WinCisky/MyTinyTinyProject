using Unity.Entities;

[GenerateAuthoringComponent]
public struct Prefabs : IComponentData
{
    public Entity CubePrefab;
    public Entity Spawner;
}
