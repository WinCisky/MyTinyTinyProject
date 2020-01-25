using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct CubeMovementStatus : IComponentData
{
#if !UNITY_DOTSPLAYER
    [Range(-1, 1)]
#endif
    public float movingSpeed;
}
