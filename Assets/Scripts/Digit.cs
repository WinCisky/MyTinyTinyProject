using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Digit : IComponentData
{
    public bool mainMenu;
    public bool isActive;
    public char shownValue;
    public int digit;
    public float3 offset;
    public Entity 
        s1_1, s2_1, s3_1,
        s1_2, s2_2, s3_2,
        s1_3, s2_3, s3_3,
        s1_4, s2_4, s3_4,
        s1_5, s2_5, s3_5;    
}
