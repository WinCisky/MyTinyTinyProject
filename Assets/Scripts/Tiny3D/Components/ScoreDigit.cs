using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ScoreDigit : IComponentData
{
    public float3 offset;
    public int digit;
}
