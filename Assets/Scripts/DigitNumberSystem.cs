using Unity.Entities;

public class DigitNumberSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Digit digit) =>
        {
            if (digit.isActive)
            {
#if !UNITY_DOTSPLAYER
                if (char.IsNumber(digit.shownValue))
                {
                    var value = char.GetNumericValue(digit.shownValue);
#else
                    
                var value = ((int)digit.shownValue - (int)'0');
                if (value <= 26)
                {
#endif
                    switch (value)
                    {
                        case 1:
                            SetVisibleDigits(digit, new bool[,]
                            {
                            { true, true, false},
                            { false, true, false},
                            { false, true, false},
                            { false, true, false},
                            { true, true, true}
                            });
                            break;
                        case 2:
                            SetVisibleDigits(digit, new bool[,]
                            {
                            { true, true, true},
                            { false, false, true},
                            { true, true, true},
                            { true, false, false},
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
                            { true, false, true},
                            { true, false, true},
                            { true, true, true},
                            { false, false, true},
                            { false, false, true}
                            });
                            break;
                        case 5:
                            SetVisibleDigits(digit, new bool[,]
                            {
                            { true, true, true},
                            { true, false, false},
                            { true, true, true},
                            { false, false, true},
                            { true, true, true}
                            });
                            break;
                        case 6:
                            SetVisibleDigits(digit, new bool[,]
                            {
                            { true, true, true},
                            { true, false, false},
                            { true, true, true},
                            { true, false, true},
                            { true, true, true}
                            });
                            break;
                        case 7:
                            SetVisibleDigits(digit, new bool[,]
                            {
                            { true, true, true},
                            { false, false, true},
                            { false, true, true},
                            { false, false, true},
                            { false, false, true}
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
                            { true, false, true},
                            { true, true, true},
                            { false, false, true},
                            { false, false, true}
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
                    switch (digit.shownValue)
                    {
                        case 'a':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, true},
                                { true, true, true},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 'b':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, false},
                                { true, false, true},
                                { true, true, true},
                                { true, false, true},
                                { true, true, false}
                            });
                            break;
                        case 'c':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, false},
                                { true, false, false},
                                { true, false, false},
                                { true, true, true}
                            });
                            break;
                        case 'd':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, false},
                                { true, false, true},
                                { true, false, true},
                                { true, false, true},
                                { true, true, false}
                            });
                            break;
                        case 'e':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, false},
                                { true, true, false},
                                { true, false, false},
                                { true, true, true}
                            });
                            break;
                        case 'f':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, false},
                                { true, true, false},
                                { true, false, false},
                                { true, false, false}
                            });
                            break;
                        case 'g':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, false},
                                { true, true, true},
                                { true, false, true},
                                { true, true, true}
                            });
                            break;
                        case 'h':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { true, true, true},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 'i':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { false, true, false},
                                { false, true, false},
                                { false, true, false},
                                { true, true, true}
                            });
                            break;
                        case 'j':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { false, false, true},
                                { false, false, true},
                                { true, false, true},
                                { true, true, true}
                            });
                            break;
                        case 'k':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { true, true, false},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 'l':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, false},
                                { true, false, false},
                                { true, false, false},
                                { true, false, false},
                                { true, true, true}
                            });
                            break;
                        case 'm':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, true, true},
                                { true, true, true},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 'n':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, true, true},
                                { true, true, true},
                                { true, true, true},
                                { true, false, true}
                            });
                            break;
                        case 'o':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, true},
                                { true, false, true},
                                { true, false, true},
                                { true, true, true}
                            });
                            break;
                        case 'p':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, true},
                                { true, true, true},
                                { true, false, false},
                                { true, false, false}
                            });
                            break;
                        case 'q':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, true},
                                { true, false, true},
                                { true, true, true},
                                { false, true, true}
                            });
                            break;
                        case 'r':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, true},
                                { true, true, false},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 's':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { true, false, false},
                                { true, true, true},
                                { false, false, true},
                                { true, true, true}
                            });
                            break;
                        case 't':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { false, true, false},
                                { false, true, false},
                                { false, true, false},
                                { false, true, false}
                            });
                            break;
                        case 'u':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { true, false, true},
                                { true, false, true},
                                { true, true, true}
                            });
                            break;
                        case 'v':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { false, true, true},
                                { false, true, true},
                                { false, false, true}
                            });
                            break;
                        case 'w':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { true, true, true},
                                { true, true, true},
                                { true, false, true}
                            });
                            break;
                        case 'x':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { false, true, false},
                                { true, false, true},
                                { true, false, true}
                            });
                            break;
                        case 'y':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, false, true},
                                { true, false, true},
                                { true, true, true},
                                { false, true, false},
                                { false, true, false}
                            });
                            break;
                        case 'z':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { true, true, true},
                                { false, false, true},
                                { false, true, false},
                                { true, false, false},
                                { true, true, true}
                            });
                            break;
                        case ':':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { false, false, false},
                                { false, true, false},
                                { false, false, false},
                                { false, true, false},
                                { false, false, false}
                            });
                            break;
                        case '/':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { false, false, true},
                                { false, true, false},
                                { false, true, false},
                                { false, true, false},
                                { true, false, false}
                            });
                            break;
                        case '!':
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { false, true, false},
                                { false, true, false},
                                { false, true, false},
                                { false, false, false},
                                { false, true, false}
                            });
                            break;
                        default: //empty space
                            SetVisibleDigits(digit, new bool[,]
                            {
                                { false, false, false},
                                { false, false, false},
                                { false, false, false},
                                { false, false, false},
                                { false, false, false}
                            });
                            break;
                    }
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
        EntityManager.SetComponentData(digit.s1_1, new DigitCube() { isActive = visible[4, 0] });
        EntityManager.SetComponentData(digit.s2_1, new DigitCube() { isActive = visible[4, 1] });
        EntityManager.SetComponentData(digit.s3_1, new DigitCube() { isActive = visible[4, 2] });
        EntityManager.SetComponentData(digit.s1_2, new DigitCube() { isActive = visible[3, 0] });
        EntityManager.SetComponentData(digit.s2_2, new DigitCube() { isActive = visible[3, 1] });
        EntityManager.SetComponentData(digit.s3_2, new DigitCube() { isActive = visible[3, 2] });
        EntityManager.SetComponentData(digit.s1_3, new DigitCube() { isActive = visible[2, 0] });
        EntityManager.SetComponentData(digit.s2_3, new DigitCube() { isActive = visible[2, 1] });
        EntityManager.SetComponentData(digit.s3_3, new DigitCube() { isActive = visible[2, 2] });
        EntityManager.SetComponentData(digit.s1_4, new DigitCube() { isActive = visible[1, 0] });
        EntityManager.SetComponentData(digit.s2_4, new DigitCube() { isActive = visible[1, 1] });
        EntityManager.SetComponentData(digit.s3_4, new DigitCube() { isActive = visible[1, 2] });
        EntityManager.SetComponentData(digit.s1_5, new DigitCube() { isActive = visible[0, 0] });
        EntityManager.SetComponentData(digit.s2_5, new DigitCube() { isActive = visible[0, 1] });
        EntityManager.SetComponentData(digit.s3_5, new DigitCube() { isActive = visible[0, 2] });
    }
}