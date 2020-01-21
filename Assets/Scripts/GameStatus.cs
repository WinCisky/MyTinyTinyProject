﻿using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct GameStatus : IComponentData
{
    public bool gameStarted;
    public double startTime;
    public int score;
}
