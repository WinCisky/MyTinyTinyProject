using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct TextRow : IComponentData
{
    public int rowNumber;
    public float3 offset;
    public Entity character;
}
