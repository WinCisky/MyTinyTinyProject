using Unity.Entities;

[GenerateAuthoringComponent]
public struct LabelNumber : IComponentData
{
    public bool IsVisible;
    public int Number;
}