using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PlayerPosition : IComponentData
{
    public float3 pos;
}
