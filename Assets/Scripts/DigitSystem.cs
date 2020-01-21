using Unity.Entities;

public class DigitSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Digit digit) =>
        {
            if (digit.isActive)
            {
                switch (digit.shownValue)
                {
                    case 1:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { false, false, true},
                            { false, false, true},
                            { false, false, true},
                            { false, false, true},
                            { false, false, true}
                        });
                        break;
                    case 2:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { true, false, false},
                            { true, true, true},
                            { false, false, true},
                            { true, true, true}
                        });
                        break;
                    case 3:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { false, false, true},
                            { false, true, true},
                            { false, false, true},
                            { true, true, true}
                        });
                        break;
                    case 4:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { false, false, true},
                            { false, false, true},
                            { true, true, true},
                            { true, false, true},
                            { true, false, true}
                        });
                        break;
                    case 5:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { false, false, true},
                            { true, true, true},
                            { true, false, false},
                            { true, true, true}
                        });
                        break;
                    case 6:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { true, false, true},
                            { true, true, true},
                            { true, false, false},
                            { true, true, true}
                        });
                        break;
                    case 7:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { false, false, true},
                            { false, false, true},
                            { false, false, true},
                            { false, false, true},
                            { true, true, true}
                        });
                        break;
                    case 8:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { true, false, true},
                            { true, true, true},
                            { true, false, true},
                            { true, true, true}
                        });
                        break;
                    case 9:
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { false, false, true},
                            { true, true, true},
                            { true, false, true},
                            { true, true, true}
                        });
                        break;
                    default: //0
                        SetVisibleDigits(digit, new bool[,]
                        {
                            { true, true, true},
                            { true, false, true},
                            { true, false, true},
                            { true, false, true},
                            { true, true, true}
                        });
                        break;
                }
            }
            else
            {
                SetVisibleDigits(digit, new bool[,]
                        {
                            { false, false, false},
                            { false, false, false},
                            { false, false, false},
                            { false, false, false},
                            { false, false, false}
                        });
            }
        });
    }

    public void SetVisibleDigits(Digit digit, bool[,] visible)
    {
        EntityManager.SetComponentData(digit.s1_1, new DigitCube() { isActive = visible[0, 0] });
        EntityManager.SetComponentData(digit.s2_1, new DigitCube() { isActive = visible[0, 1] });
        EntityManager.SetComponentData(digit.s3_1, new DigitCube() { isActive = visible[0, 2] });
        EntityManager.SetComponentData(digit.s1_2, new DigitCube() { isActive = visible[1, 0] });
        EntityManager.SetComponentData(digit.s2_2, new DigitCube() { isActive = visible[1, 1] });
        EntityManager.SetComponentData(digit.s3_2, new DigitCube() { isActive = visible[1, 2] });
        EntityManager.SetComponentData(digit.s1_3, new DigitCube() { isActive = visible[2, 0] });
        EntityManager.SetComponentData(digit.s2_3, new DigitCube() { isActive = visible[2, 1] });
        EntityManager.SetComponentData(digit.s3_3, new DigitCube() { isActive = visible[2, 2] });
        EntityManager.SetComponentData(digit.s1_4, new DigitCube() { isActive = visible[3, 0] });
        EntityManager.SetComponentData(digit.s2_4, new DigitCube() { isActive = visible[3, 1] });
        EntityManager.SetComponentData(digit.s3_4, new DigitCube() { isActive = visible[3, 2] });
        EntityManager.SetComponentData(digit.s1_5, new DigitCube() { isActive = visible[4, 0] });
        EntityManager.SetComponentData(digit.s2_5, new DigitCube() { isActive = visible[4, 1] });
        EntityManager.SetComponentData(digit.s3_5, new DigitCube() { isActive = visible[4, 2] });
    }
}