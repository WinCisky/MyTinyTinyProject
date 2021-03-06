﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Tiny;

public class MenuSpawner : ComponentSystem
{
    private void SetDigit(Entity instance, char c, int characters, int index, float yPos, int row)
    {
        var pos = new float3((index - (characters / 2f)) * 0.8f, yPos, 0);

        var digit = EntityManager.GetComponentData<Digit>(instance);
        digit.shownValue = c;
        digit.isActive = true;
        digit.charIndex = index;
        digit.row = row;
        digit.offset = new float3(pos.x, pos.y, -5);
        EntityManager.SetComponentData(instance, digit);


        EntityManager.SetComponentData(instance, new Translation() { Value = pos });
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref TextRow textRow) =>
            {
                switch (textRow.rowNumber)
                {
                    case 1:
                        var s1 = "my tiny tiny project";
                        for (int i = 0; i < 20; i++)
                        {
                            var instance = EntityManager.Instantiate(textRow.character);
                            SetDigit(instance, s1[i], 20, i, 5, textRow.rowNumber);
                        }
                        break;
                    case 2:
                        var s2 = "created by";
                        for (int i = 0; i < 10; i++)
                        {
                            var instance = EntityManager.Instantiate(textRow.character);
                            SetDigit(instance, s2[i], 10, i, 3, textRow.rowNumber);
                        }
                        break;
                    case 3:
                        var s3 = "simone simonella";
                        for (int i = 0; i < 16; i++)
                        {
                            var instance = EntityManager.Instantiate(textRow.character);
                            SetDigit(instance, s3[i], 16, i, 1, textRow.rowNumber);
                        }
                        break;
                    case 4:
                        var s4 = "touch/click/press space";
                        for (int i = 0; i < 23; i++)
                        {
                            var instance = EntityManager.Instantiate(textRow.character);
                            SetDigit(instance, s4[i], 23, i, -3, textRow.rowNumber);
                        }
                        break;
                    default:
                        var s5 = "to start";
                        for (int i = 0; i < 8; i++)
                        {
                            var instance = EntityManager.Instantiate(textRow.character);
                            SetDigit(instance, s5[i], 8, i, -5, textRow.rowNumber);
                        }
                        break;
                }

                EntityManager.RemoveComponent(entity, typeof(TextRow));
                //commandBuffer.DestroyEntity(entityInQueryIndex, entity); //do not destroy, only hide/show for now
            });
    }
}
