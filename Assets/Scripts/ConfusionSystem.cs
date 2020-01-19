using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class ConfusionSystem : ComponentSystem
{
    float2 rotate_point(float ox, float oy, float angle, float px, float py)
    {
        var cosTheta = math.cos(angle);
        var sinTheta = math.sin(angle);
        var pxox = px - ox;
        var pyoy = py - oy;

        //p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
        var xt = cosTheta * pxox - sinTheta * pyoy + ox;
        //p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy
        var yt = sinTheta * pxox + cosTheta * pyoy + oy;

        return new float2(xt, yt);

    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var elapsedTime = Time.ElapsedTime;
        Entities.ForEach((ref Confusion confusion) =>
        {
            if ((!confusion.slowDownTime && !confusion.speedUpTime) && !confusion.rotate)
            {
                confusion.currentElapsedTime += deltaTime;
                //Debug.Log(confusion.currentElapsedTime + " " + confusion.confusionCooldown);
                if (confusion.currentElapsedTime > confusion.confusionCooldown)
                {
                    //slow down -> decide if reverse or rotate later
                    confusion.slowDownTime = true;
                }
            }
            else
            {
                if (confusion.slowDownTime)
                {
                    bool finished = false;
                    Entities.ForEach((ref CubeMovementStatus cms) =>
                    {
                        var direction = cms.movingSpeed > 0 ? 1 : -1;
                        var ms = cms.movingSpeed - (deltaTime * direction)/2.0f;
                        if ((ms <= 0 && direction >= 0) || (ms >= 0 && direction <= 0))
                        {
                            cms.movingSpeed = 0;
                            finished = true;
                        }
                        else
                            cms.movingSpeed = ms;
                    });
                    //start speed up or rotate
                    if (finished)
                    {
                        //FOR DEVELOPMENT -> only rotate
                        confusion.rotate = true;
                        //set target rotation
                        confusion.targetRotation = 0;

                        //DEVELOPMENT BAN
                        /*
                        //50% chance of rotating
                        if (noise.cnoise(new float2((float)elapsedTime, (float)elapsedTime) * 0.21F) > 0)
                        {
                            if (noise.cnoise(new float2((float)elapsedTime, (float)elapsedTime) * 0.22F) > 0)
                            {
                                confusion.speedUpTime = true;
                            }
                            else
                            {
                                //TODO: something different
                                confusion.speedUpTime = true;
                            }
                        }
                        else// rotations
                        {
                            if (noise.cnoise(new float2((float)elapsedTime, (float)elapsedTime) * 0.22F) > 0)
                            {
                                confusion.fakeRotate = true;
                                //set target rotation
                                confusion.targetRotation = 0;
                            }
                            else
                            {
                                confusion.rotate = true;
                                //set target rotation
                                confusion.targetRotation = 0;
                            }
                        }
                        */

                        confusion.slowDownTime = false;
                        //swap with reverse
                        Entities.ForEach((ref ObstacleTag ot) =>
                        {
                            ot.Swapped = false;
                        });
                    }
                }
                else if (confusion.speedUpTime)
                {
                    bool finished = false;
                    bool dir = confusion.wasGoingDown;
                    Entities.ForEach((ref CubeMovementStatus cms) =>
                    {
                        var direction = dir ? 1 : -1;
                        var ms = cms.movingSpeed - (deltaTime * direction)/2.0f;
                        if (math.abs(ms) >= 1)
                        {
                            cms.movingSpeed = -direction;
                            finished = true;
                        }
                        else
                            cms.movingSpeed = ms;
                    });
                    //update confusion values
                    if (finished)
                    {
                        confusion.wasGoingDown = !confusion.wasGoingDown;
                        confusion.speedUpTime = false;
                        confusion.currentElapsedTime = 0;
                    }
                }
                else if(confusion.fakeRotate) //rotate
                {

                    confusion.targetRotation++;
                    //TODO: logic for turning all entities -> kinda complicated, such cool tho
                    Entities.WithAll<ObstacleTag>().ForEach((ref Rotation rotation) =>
                    {
                        rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(1)));
                    });
                    if (confusion.targetRotation >= 90)
                    {
                        confusion.fakeRotate = false;
                        confusion.currentElapsedTime = 0;
                        confusion.wasGoingDown = !confusion.wasGoingDown;
                        confusion.speedUpTime = true;
                    }
                }else if (confusion.rotate)
                {
                    if(confusion.targetRotation == 0)
                    {
                        //calculate resulting rotation in order to reduce floating point errors
                        //the rotation process is a sum of small numbers
                        var pp = new float2(0, 0);
                        Entities.WithAll<Player>().ForEach((ref Translation translation) =>
                        {
                            pp = new float2(translation.Value.x, translation.Value.y);
                        });
                        Entities.ForEach((ref Translation translation, ref ObstacleTag ot) =>
                        {
                            var target = rotate_point(pp.x, pp.y, math.radians(90), translation.Value.x, translation.Value.y);
                            ot.targetPos = new float2(target.x, ot.startPos.y + (target.y - translation.Value.y));
                        });
                    }
                    //to check
                    confusion.targetRotation++;
                    var playerPos = new float2(0, 0);
                    Entities.WithAll<Player>().ForEach((ref Translation translation) =>
                    {
                        playerPos = new float2(translation.Value.x, translation.Value.y);
                    });
                    Entities.ForEach((ref Rotation rotation, ref Translation translation, ref ObstacleTag ot) =>
                    {
                        rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(1)));
                        var newPos = rotate_point(playerPos.x, playerPos.y, 0.01745f, translation.Value.x, translation.Value.y);
                        //translation.Value = new float3(newPos.x, newPos.y, translation.Value.z);
                        ot.startPos.x = newPos.x;
                        ot.startPos.y += (newPos.y - translation.Value.y);
                    });
                    if (confusion.targetRotation >= 90)
                    {
                        confusion.rotate = false;
                        confusion.currentElapsedTime = 0;
                        confusion.wasGoingDown = !confusion.wasGoingDown;
                        confusion.speedUpTime = true;

                        Entities.ForEach((ref ObstacleTag ot) =>
                        {
                            ot.startPos.x = ot.targetPos.x;
                            ot.startPos.y = ot.targetPos.y;
                        });
                    }

                }//can continue with something else maybe
            }
        });
    }
}