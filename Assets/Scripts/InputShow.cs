using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny;
#if UNITY_DOTSPLAYER
using System;
using Unity.Tiny.Input;

#else
using UnityEngine;
#endif

[AlwaysSynchronizeSystem]
[AlwaysUpdateSystem]
public class InputShow : ComponentSystem
{
    protected override void OnUpdate()
    {
        bool actionOne = false;
        bool actionTwo = false;

#if UNITY_DOTSPLAYER
            var Input = World.GetExistingSystem<InputSystem>();
#endif

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            Entities.WithAll<Player>().ForEach((ref PlayerInput pi) => {
                pi.actionA = true;
            });
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            Entities.WithAll<Player>().ForEach((ref PlayerInput pi) => {
                pi.actionB = true;
            });
#if !UNITY_DOTSPLAYER //Editor
        for (int i = 0; i < Input.touchCount; i++)
        {
            var pos = Input.GetTouch(i).position;
            var phase = Input.GetTouch(i).phase;

            //if (phase == TouchPhase.Began)
            //{
                actionOne = pos.x > (Screen.width / 2) ? true : false;
                actionTwo = !actionOne;

                //set player input
                Entities.WithAll<Player>().ForEach((ref PlayerInput pi) =>
                {
                    if (actionOne)
                        pi.actionA = actionOne;
                    else
                        pi.actionB = actionTwo;
                });
            //}
        }

        
#else //Actual device
        var di = GetSingleton<DisplayInfo>();

        // TODO currently rendering is done with 1080p, with aspect kept.
        // We might not be using the actual width.  DisplayInfo needs to get reworked.
        var height = di.height;
        int width = di.width;
        float targetRatio = 1920.0f / 1080.0f;
        float actualRatio = (float)width / (float)height;
        

        if (Input.IsTouchSupported() && Input.TouchCount() > 0)
            {
            for (var i = 0; i < Input.TouchCount(); i++)
            {
                var itouch = Input.GetTouch(i);
                //if (itouch.phase == TouchState.Began)
                //{
                    var pos = new float2(itouch.x, itouch.y);

                    if (actualRatio > targetRatio)
                    {
                        width = (int)(di.height * targetRatio);
                        pos.x -= (di.width - width) / 2.0f;
                    }

                    actionOne = pos.x > (width / 2) ? true : false;
                    actionTwo = !actionOne;

                    //set player input
                    Entities.WithAll<Player>().ForEach((ref PlayerInput pi) =>
                    {
                        if (actionOne)
                            pi.actionA = actionOne;
                        else
                            pi.actionB = actionTwo;
                    });
                //}
            }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    var xpos = (int) Input.GetInputPosition().x;

                    if (actualRatio > targetRatio)
                    {
                        width = (int)(di.height * targetRatio);
                        xpos -= (int)((di.width - width) / 2.0f);
                    }

                actionOne = xpos > (width / 2) ? true : false;
                    actionTwo = !actionOne;

                    //set player input
                    Entities.WithAll<Player>().ForEach((ref PlayerInput pi) => {
                        if (actionOne)
                            pi.actionA = actionOne;
                        else
                            pi.actionB = actionTwo;
                    });
                }
            }
#endif

    }
}