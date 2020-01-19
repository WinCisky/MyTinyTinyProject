using System;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ObstacleTag : IComponentData
{
    public float MovementSpeed;
    public bool Swapped;
    public double StartTime;
    public float3 startPos;

    public float2 targetPos;
}
